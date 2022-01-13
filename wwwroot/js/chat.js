

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

    connection.invoke("getConnectionId")
        .then(function (connectionId) {
            sessionStorage.setItem('connectionId', connectionId);
        }).catch(err => console.error(err.toString()));
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = "test";
    var text = document.getElementById("messageInput").value;
    console.log("sending...")

    document.getElementById("messageInput").value = "";
    let message = new Message(user, text, Date.now);
    console.log(`sending ${message} says ${message.text}`);
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("ReceiveMessage", function (message) {
    console.log(`${message.username} says ${message.text}`);
    var li = document.createElement("li");
    li.setAttribute("class", "msg-left")
    var divFirst = document.createElement("div");
    divFirst.setAttribute("class", "msg-left")
    var divSecond = document.createElement("div");
    divSecond.setAttribute("class", "msg-left-sub")
    var divThird = document.createElement("div");
    divThird.setAttribute("class", "msg-desc")
    var small = document.createElement("small");

    li.appendChild(divFirst);
    divFirst.appendChild(divSecond);
    divSecond.appendChild(divThird);
    divSecond.appendChild(small);

    divThird.textContent = `${message.username} says ${message.text}`;

    document.getElementById("messageList").appendChild(li)
    console.log(`${message.Username} says ${message.Text}`);
});