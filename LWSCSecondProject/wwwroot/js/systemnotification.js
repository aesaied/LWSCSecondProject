
"use strict";

var notificationConnection = new signalR.HubConnectionBuilder().withUrl("/notification").build();


notificationConnection.on("OnNotify", function (message) {

    alert(message);
});


notificationConnection.start().then(function () {
    
}).catch(function (err) {
   
});


function sendNotifcation(msg) {
    notificationConnection.invoke("Notify", msg).catch(function (err) {
        return console.error(err.toString());
    });
}