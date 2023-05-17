const params = new URLSearchParams(window.location.search);
var queryuser = params.get('user');
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7107/hub/chat?user=" + queryuser)
    .configureLogging(signalR.LogLevel.Debug)
    .withAutomaticReconnect()
    .build();
connection.serverTimeoutInMilliseconds = 1600000; // 3 mins
document.getElementById("sendButton").disabled = true;

async function start() {
    try {
        await connection.start();
        console.assert(connection.state === signalR.HubConnectionState.Connected);
        document.getElementById("sendButton").disabled = false;
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        console.assert(connection.state === signalR.HubConnectionState.Disconnected);

        //setTimeout(start, 5000);
    }
};

connection.onclose(error => {
    console.assert(connection.state === signalR.HubConnectionState.Disconnected);

    document.getElementById("messageInput").disabled = true;

    const li = document.createElement("li");
    li.textContent = `Connection closed due to error "${error}". Try refreshing this page to restart the connection.`;
    document.getElementById("messageList").appendChild(li);
});

// Start the connection.
start();

connection.on("ReceiveMessageThread", (messages) => {
    const li = document.createElement("li");
    li.textContent = `${messages}`;
    document.getElementById("messageList").appendChild(li);
});
connection.on("ReceiveMessage", (senderUsername, content) => {
    const li = document.createElement("li");
    li.textContent = `${senderUsername}: ${content}`;
    document.getElementById("messageList").appendChild(li);
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var RecipientUsername = document.getElementById("userInput").value;
    var Content = document.getElementById("messageInput").value;
   
    connection.invoke("SendMessage", RecipientUsername, Content).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.onreconnected(connectionId => {
    console.assert(connection.state === signalR.HubConnectionState.Connected);

    document.getElementById("messageInput").disabled = false;

    const li = document.createElement("li");
    li.textContent = `Connection reestablished. Connected with connectionId "${connectionId}".`;
    document.getElementById("messageList").appendChild(li);
});

