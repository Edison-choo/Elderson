"use strict";

class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    li.classList.add("msg-left")
    var divFirst = document.createElement("div");
    divFirst.classList.add("msg-left")
    var divSecond = document.createElement("div");
    divSecond.classList.add("msg-left-sub")
    var divThird = document.createElement("div");
    divThird.classList.add("msg-desc")
    var small = document.createElement("small");

    li.appendChild(divFirst);
    divFirst.appendChild(divSecond);
    divSecond.appendChild(divThird, small);

    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    divThird.textContent = `${message.Username} says ${message.Text}`;

    console.log(`${message.Username} says ${message.Text}`);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = "test";
    var text = document.getElementById("messageInput").value;
    console.log("sending...")

    let message = new Message(user, text);
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});