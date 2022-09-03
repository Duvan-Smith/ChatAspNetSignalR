"use strict";

var conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

conexion.on("RecibirMensaje", function (mensaje) {
  var li = document.createElement("li");
  li.textContent = mensaje.usuario + "-" + mensaje.contenido;
  document.getElementById("lstMensajes").appendChild(li);
});

document
  .getElementById("btnConectar")
  .addEventListener("click", function (event) {
    event.preventDefault();
    if (conexion.state === signalR.HubConnectionState.Disconnected) {
      conexion
        .start()
        .then(function () {
          var li = document.createElement("li");
          li.textContent = "Conectado";
          document.getElementById("lstMensajes").appendChild(li);
          document.getElementById("btnConectar").value = "Desconectar";
          document.getElementById("txtUsuario").disabled = true;
          document.getElementById("btnEnviar").disabled = false;

          var usuario = document.getElementById("txtUsuario").value;
          var sala = document.getElementById("sala").value;

          conexion
            .invoke("EnviarMensaje", { usuario, contenido: "", sala })
            .catch(function (error) {
              console.error(error);
            });

          document.getElementById("txtMensaje").value = "";
        })
        .catch(function (error) {
          console.error(error);
        });
    } else if (conexion.state === signalR.HubConnectionState.Connected) {
      conexion.stop();
      var li = document.createElement("li");
      li.textContent = "Desconectado";
      document.getElementById("lstMensajes").appendChild(li);
      document.getElementById("btnConectar").value = "Conectar";
      document.getElementById("txtUsuario").disabled = false;
    }
  });

document
  .getElementById("btnEnviar")
  .addEventListener("click", function (event) {
    event.preventDefault();
    if (conexion.state !== signalR.HubConnectionState.Connected) {
      return;
    }
    var usuario = document.getElementById("txtUsuario").value;
    var contenido = document.getElementById("txtMensaje").value;
    var sala = document.getElementById("sala").value;

    conexion
      .invoke("EnviarMensaje", { usuario, contenido, sala })
      .catch(function (error) {
        console.error(error);
      });

    document.getElementById("txtMensaje").value = "";
  });
