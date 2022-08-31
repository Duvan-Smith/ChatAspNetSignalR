"use strict";

var conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

conexion.on("RecibirMensaje", function ({ usuario, contenido }) {
    var li = document.createElement("li");
    li.textContent = usuario + "-" + contenido;
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
    var usuario = document.getElementById("txtUsuario").value;
    var contenido = document.getElementById("txtMensaje").value;

    conexion.invoke("EnviarMensaje", usuario, contenido )
        .catch(function (error) {
            console.error(error);
        });

    document.getElementById("txtMensaje").value = "";
})