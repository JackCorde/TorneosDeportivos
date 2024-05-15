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
        case 'usuarios':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerUsuarios')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let usuarios = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar usuarios...">
                    <button id="addUserButton">Agregar Usuario</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Completo</th>
                            <th>Correo Electrónico</th>
                            <th>Nombre de Usuario</th>
                            <th>Rol</th>
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
                            <td>${usuario.rolName}</td>
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:red;'>
                                    Eliminar
                                </a>
                                                               
                            </td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay usuarios cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Encargados':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerUsuariosPorRol?rol=6')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let usuarios = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar encargados...">
                    <button id="addUserButton">Agregar Encargado</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Completo</th>
                            <th>Correo Electrónico</th>
                            <th>Nombre de Usuario</th>
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
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:red;'>
                                    Eliminar
                                </a>
                                                               
                            </td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay encargados cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Arbitros':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
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
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:purple;'>
                                    Partidos
                                </a>
                                                               
                            </td>
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
        case 'Coachs':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerUsuariosPorRol?rol=4')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let usuarios = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar coachs...">
                    <button id="addUserButton">Agregar Coach</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Completo</th>
                            <th>Correo Electrónico</th>
                            <th>Nombre de Usuario</th>
                            <th>Deporte</th>
                            <th>Equipo</th>
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
                            <td>${usuario.equipo}</td>
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:red;'>
                                    Eliminar
                                </a>
                                                               
                            </td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay coachs cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Canchas':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerCanchas')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let canchas = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterCanchas()" placeholder="Buscar canchas...">
                    <button id="addUserButton">Agregar Cancha</button>
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
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${cancha.canchaId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${cancha.canchaId}" style='border:none; background-color:transparent; cursor:pointer; color:red;'>
                                    Eliminar
                                </a>
                                                               
                            </td>
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

        case 'Contadores':
            // Realizar una solicitud AJAX para obtener los datos de los usuarios desde el servidor
            fetch('/Consultas/ObtenerUsuariosPorRol?rol=2')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let usuarios = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar contadores...">
                    <button id="addUserButton">Agregar Contador</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Completo</th>
                            <th>Correo Electrónico</th>
                            <th>Nombre de Usuario</th>
                            <th>Cuentas Responsable</th>
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
                            <td>${usuario.numeroCuentas}</td>
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${usuario.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:purple;'>
                                    Cuentas
                                </a>
                                                               
                            </td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay contadores cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los usuarios:', error));
            break;
        case 'Torneos':
            fetch('/Consultas/ObtenerTorneos')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let torneos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar torneos...">
                    <button id="addUserButton">Agregar Torneo</button>
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
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${torneo.usuarioId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${torneo.usuarioId}" style='border:none; background-color:transparent; cursor:pointer; color:purple;'>
                                    Partidos
                                </a>
                                                               
                            </td>
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
        case 'Equipos':
            fetch('/Consultas/ObtenerEquipos')
                .then(response => response.json())
                .then(data => {
                    // Manipular los datos recibidos y construir la tabla
                    let equipos = data;
                    let html = `
                <div id="searchContainer">
                    <input type="text" id="searchInput" onkeyup="filterTable()" placeholder="Buscar equipos...">
                    <button id="addUserButton">Agregar Equipo</button>
                </div>
                <table id="userTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Equipo</th>
                            <th>Deporte</th>
                            <th>Categoria</th>
                            <th>Coach Responsable</th>
                            <th>Ganador</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    if (equipos && equipos.length > 0) {
                        equipos.forEach(equipo => {
                            html += `
                        <tr>
                            <td>${equipo.equipoId}</td>
                            <td>${equipo.equipoNombre}</td>
                            <td>${equipo.deporte}</td>
                            <td>${equipo.categoria}</td>
                            <td>${equipo.coachNombre}</td>
                            <td>${equipo.torneoActual}</td>
                            <td>
                                <a href="/Administrador/EditarUsuario?id=${equipo.equipoId}" style='border:none; background-color:transparent; cursor:pointer;'>
                                    Editar
                                </a>
                            </td>
                            <td>
                                <a href="/Administrador/EliminarUsuario?id=${equipo.equipoId}" style='border:none; background-color:transparent; cursor:pointer; color:purple;'>
                                    Partidos
                                </a>
                                                               
                            </td>
                        </tr>`;
                        });
                    } else {
                        html += `
                    <tr>
                        <td colspan="4">No hay equipos cargados</td>
                    </tr>`;
                    }
                    html += `</tbody></table>`;
                    mainContent.innerHTML = html;
                })
                .catch(error => console.error('Error al obtener los coachs:', error));
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
