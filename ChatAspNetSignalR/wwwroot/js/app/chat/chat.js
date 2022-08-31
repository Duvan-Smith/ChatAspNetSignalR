"use strict";

var conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

conexion.on("RecibirMensaje", function (user, mensaje) {
    var li = document.createElement("li");
    li.textContent = user + "-" + mensaje;
    document.getElementById("lstMensajes").appendChild(li);
})

conexion.start().then(function () {
    var li = document.createElement("li");
    li.textContent = "Inicio";
    document.getElementById("lstMensajes").appendChild(li);
}).catch(function (error) {
    console.error(error);
})

document.getElementById("btnEnviar").addEventListener("click", function (event) {
    event.preventDefault();
    var user = document.getElementById("txtUsuario").value;
    var mensaje = document.getElementById("txtMensaje").value;

    conexion.invoke("EnviarMensaje", user, mensaje)
        .catch(function (error) {
            console.error(error);
        });

    document.getElementById("txtMensaje").value = "";
})