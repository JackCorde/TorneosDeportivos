// Ejecutar función en el evento click
document.getElementById("btn_open").addEventListener("click", open_close_menu);

// Declaramos variables
var side_menu = document.getElementById("menu_side");
var body = document.getElementById("body");

// Evento para mostrar y ocultar menú
function open_close_menu() {
    body.classList.toggle("body_move");
    side_menu.classList.toggle("menu__side_move");
}

// Si el ancho de la página es menor a 760px, ocultará el menú al recargar la página
if (window.innerWidth < 760) {
    body.classList.add("body_move");
    side_menu.classList.add("menu__side_move");
}

// Haciendo el menú responsive (adaptable)
window.addEventListener("resize", function() {
    if (window.innerWidth > 760) {
        body.classList.remove("body_move");
        side_menu.classList.remove("menu__side_move");
    }
    if (window.innerWidth < 760) {
        body.classList.add("body_move");
        side_menu.classList.add("menu__side_move");
    }
});

function showContent(option) {
    var mainContent = document.getElementById('main_content');
    switch(option) {  
        case 'Torneos':
            fetch('/Consultas/ObtenerTorneos')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let torneos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar torneos...">
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Torneo</th>
                            <th>Deporte</th>
                            <th>Categoria</th>
                            <th>Fecha Inicio</th>
                            <th>Fecha Final</th>
                            <th>Ganador</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (torneos && torneos.length > 0) {
                        torneos.forEach(torneo => {
                            html += `
                        <tr>
                            <td>${torneo.torneoId}</td>
                            <td>${torneo.torneoNombre}</td>
                            <td>${torneo.deporte}</td>
                            <td>${torneo.categoria}</td>
                            <td>${torneo.fechaInicio}</td>
                            <td>${torneo.fechaFinal}</td>
                            <td>${torneo.ganador}</td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay torneos cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Lugares':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerCanchas')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let canchas = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar canchas...">
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Descripcion </th>
                            <th>Deporte</th>
                            <th>Activo</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (canchas && canchas.length > 0) {
                        canchas.forEach(cancha => {
                            var status = cancha.activa === true ? 'Activa' : 'Inactiva';
                            html += `
                        <tr>
                            <td>${cancha.canchaId}</td>
                            <td>${cancha.descripcion}</td>
                            <td>${cancha.deporte}</td>
                            <td>${status}</td>
                           
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay canchas cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener las canchas:', error));
            break;
        case 'Arbitros':
            fetch('/Consultas/ObtenerUsuariosPorRol?rol=3')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let usuarios = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar árbitros...">
                    <button id="addUserButton">Agregar Árbitro</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Completo</th>
                            <th>Correo Electrónico</th>
                            <th>Nombre de Usuario</th>
                            <th>Deporte</th>
                            <th>Partidos</th>
                            <th>Honorarios</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (usuarios && usuarios.length > 0) {
                        usuarios.forEach(usuario => {
                            html += `
                        <tr>
                            <td>${usuario.usuarioId}</td>
                            <td>${usuario.nombre}</td>
                            <td>${usuario.email}</td>
                            <td>${usuario.username}</td>
                            <td>${usuario.deporte}</td>
                            <td>${usuario.numeroPartidos}</td>
                            <td>${usuario.costo}</td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay árbitros cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Resultados':
            fetch('/Consultas/ObtenerPartidos')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let partidos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar resultados...">
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Equipo Ganador </th>
                            <th>Equipo Perdedor </th>
                            <th>Torneo </th>
                            <th>Fecha </th>
                            <th>Arbitro </th>
                            <th>Resultados</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (partidos && partidos.length > 0) {
                        partidos.forEach(partido => {

                            html += `
                        <tr>
                            <td>${partido.partidoId}</td>
                            <td>${partido.equipoG}</td>
                            <td>${partido.equipoP}</td>
                            <td>${partido.torneo}</td>
                            <td>${partido.fecha}</td>
                            <td>${partido.arbitro}</td>
                            <td>${partido.resultados}</td>
                           
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay canchas cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener las canchas:', error));
            break;
        case 'Partidos':
            fetch('/Consultas/ObtenerPartidos')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let partidos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar resultados...">
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Equipo Local </th>
                            <th>Equipo Visitante </th>
                            <th>Torneo </th>
                            <th>Cancha </th>
                            <th>Fecha </th>
                            <th>Hora </th>
                            <th>Arbitro </th>
                            <th>Costo de Inscripción</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (partidos && partidos.length > 0) {
                        partidos.forEach(partido => {
                            
                            html += `
                        <tr>
                            <td>${partido.partidoId}</td>
                            <td>${partido.equipoL}</td>
                            <td>${partido.equipoV}</td>
                            <td>${partido.torneo}</td>
                            <td>${partido.cancha}</td>
                            <td>${partido.fecha}</td>
                            <td>${partido.hora}</td>
                            <td>${partido.arbitro}</td>
                            <td>${partido.costoTotal}</td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay canchas cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los partidos:', error));
            break;
        case 'Partidos Contador':
            fetch('/Consultas/ObtenerPagosPartido')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let partidos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar pagos...">
                    <button id="addUserButton">Realizar Pago</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>ID Cuenta </th>
                            <th>ID Partido </th>
                            <th>Torneo </th>
                            <th>Cantidad </th>
                            <th>Fecha </th>
                            <th>Equipo Local </th>
                            <th>Equipo Visitante </th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (partidos && partidos.length > 0) {
                        partidos.forEach(partido => {

                            html += `
                        <tr>
                            <td>${partido.pagoPartidoId}</td>
                            <td>${partido.cuentaId}</td>
                            <td>${partido.partidoId}</td>
                            <td>${partido.torneo}</td>
                            <td>${partido.cantidad}</td>
                            <td>${partido.fechaPago}</td>
                            <td>${partido.equipoLocal}</td>
                            <td>${partido.equipoVisitante}</td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay pagos de partidos cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los partidos:', error));
            break;
                case 'Partidos Arbitro':
                    fetch('/Consultas/ObtenerPartidosPorArbitro')
                        .then(response => response.json())
                        .then(data => {
                            // Manipular los datos recibidos y construir la tabla
                            let partidos = data;
                            let html = `
                        <div id="searchContainer">
                            <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar Partidos...">
                            <button id="addUserButton">Subir Resultados</button>
                        </div>
                        <table id="userTable">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Equipo Local </th>
                                    <th>Equipo Visitante </th>
                                    <th>Torneo </th>
                                    <th>Cancha </th>
                                    <th>Fecha </th>
                                    <th>Hora </th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody>`;
                            if (partidos && partidos.length > 0) {
                                partidos.forEach(partido => {

                                    html += `
                                <tr>
                                    <td>${partido.partidoId}</td>
                                    <td>${partido.equipoL}</td>
                                    <td>${partido.equipoV}</td>
                                    <td>${partido.torneo}</td>
                                    <td>${partido.cancha}</td>
                                    <td>${partido.fecha}</td>
                                    <td>${partido.hora}</td>
                                    <td>${partido.status}</td>
                           
                                </tr>`;
                                });
                            } else {
                                html += `
                            <tr>
                                <td colspan="4">No hay canchas cargados</td>
                            </tr>`;
                            }
                            html += `</tbody></table>`;
                            mainContent.innerHTML = html;
                        })
                        .catch(error => console.error('Error al obtener las canchas:', error));
                    break;
                case 'Inscripciones':
            fetch('/Consultas/ObtenerCuentas')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let cuentas = data;
                    let html = `
                        <div id="searchContainer">
                            <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar cuentas...">
                        </div>
                        <table id="userTable">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Torneo </th>
                                    <th>Contador</th>
                                    <th>Depositos</th>
                                    <th>Retiros</th>
                                    <th>Total De Dinero</th>
                                </tr>
                            </thead>
                            <tbody>`;
                    if (cuentas && cuentas.length > 0) {
                        cuentas.forEach(cuenta => {

                            html += `
                                <tr>
                                    <td>${cuenta.cuentaId}</td>
                                    <td>${cuenta.torneo}</td>
                                    <td>${cuenta.contador}</td>
                                    <td>${cuenta.depositos}</td>
                                    <td>${cuenta.retiros}</td>
                                    <td>${cuenta.total}</td>
                           
                                </tr>`;
                        });
                    } else {
                        html += `
                            <tr>
                                <td colspan="4">No hay canchas cargados</td>
                            </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener las canchas:', error));
            break;
        // Agrega más casos según las opciones del menú
    }
}

// Función para filtrar la tabla de usuarios
function filterTable() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("searchInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("userTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1]; // Index 1 is Nombre column
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }       
    }
}

function filterCanchas() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("searchInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("userTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1]; // Index 1 is Nombre column
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

