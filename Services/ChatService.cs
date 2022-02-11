using Elderson.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class ChatService
    {
        private EldersonContext _context;

        public ChatService(EldersonContext context)
        {
            _context = context;
        }
        public Dictionary<string, List<Message>> GetAllChats(string userId)
        {
            List<Message> AllMessages = new List<Message>();
            Dictionary<string, List<Message>> SeparatedMessages = new Dictionary<string, List<Message>>();
            AllMessages = _context.Messages.Where(e => e.UserId == userId | e.ToUserId == userId).ToList();
            
            foreach (var msg in AllMessages)
            {
                if (msg.ToUserId == userId)
                {
                    if (SeparatedMessages.ContainsKey(msg.UserId))
                    {
                        SeparatedMessages[msg.UserId].Add(msg);
                    }
                    else
                    {
                        SeparatedMessages[msg.UserId] = new List<Message>() { msg };
                    }
                    SeparatedMessages[msg.UserId] = SeparatedMessages[msg.UserId].OrderBy(e => e.When).ToList();
                } 
                else
                {
                    if (SeparatedMessages.ContainsKey(msg.ToUserId))
                    {
                        SeparatedMessages[msg.ToUserId].Add(msg);
                    }
                    else
                    {
                        SeparatedMessages[msg.ToUserId] = new List<Message>() { msg };
                    }
                    SeparatedMessages[msg.ToUserId] = SeparatedMessages[msg.ToUserId].OrderBy(e => e.When).ToList();
                }
                
                
            }

            return SeparatedMessages;
        }

        private bool ChatExists(string id)
        {
            return _context.Incident.Any(e => e.Id == id);
        }

        public bool AddChat(Message Chat)
        {
            if (ChatExists(Chat.Id))
            {
                return false;
            }
            _context.Add(Chat);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateChat(Message Chat)
        {
            bool updated = true;
            _context.Attach(Chat).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(Chat.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }

        public bool DeleteChat(Message theChat)
        {
            bool deleted = true;
            _context.Attach(theChat).State = EntityState.Modified;

            try
            {
                _context.Remove(theChat);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(theChat.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }
    }
}
