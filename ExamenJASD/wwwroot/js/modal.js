//variable global para la instancia de bootstrap
let myModal;

document.addEventListener('DOMContentLoaded', () => {

    const modalElement = document.getElementById('formModal');

    //si existe el modal
    if (modalElement)
    {
            myModal = new bootstrap.Modal(modalElement, {
            backdrop: 'static',
            keyboard: false
    });

        //metodo para cargar usuarios con ajax al cargar el DOM
        cargarUsuarios();

        //Boton para abrir el modal
        document.getElementById('botonAgregar').addEventListener('click', () => {

            //Resetea el formulario
            document.getElementById('formUsuario').reset();
            document.querySelector('#formUsuario input[name="iId"]').value = '';

            //Muestra el modal
            myModal.show();
        });
    }
});

function cargarUsuarios() {
    $.ajax({
        //apunta al handler OnGetListaUsuarios de ListaUsuarios.cshtml
        url: '?handler=ListaUsuarios',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            if (data.noAuth) {
                window.location.href = "/Login/Login";
                return;
            }

            var html = '';
            data.forEach(usuario => {
                html += `<tr>
                    <td>${usuario.iId}</td>
                    <td>${usuario.sUsuario}</td>
                    <td>${usuario.sNombre}</td>
                    <td>${usuario.sEmail}</td>
                    <td>${usuario.sEstatus}</td>
                    <td>${usuario.dtFechaAlta}</td>
                    <td>${usuario.dtFechaModificacion}</td>
                    <td>
                        <button class="btn btn-warning btnEditar" id="btnEditar" data-id="${usuario.iId}"> <i id="iconEditar" class="fa-solid fa-pen-to-square"></i> Editar</button>
                    </td>
                </tr>`;
            });
            $('#tablaUsuarios tbody').html(html);
        }
    });
}

function validarForm() {
    const errores = [];
    const sNombre = $('#formUsuario input[name="sNombre"]').val().trim();
    const sUsuario = $('#formUsuario input[name="sUsuario"]').val().trim();
    const sPassword = $('#formUsuario input[name="sPassword"]').val().trim();
    const sEmail = $('#formUsuario input[name="sEmail"]').val().trim();
    const iId = $('#formUsuario input[name="iId"]').val().trim();

    const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    const regexPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{14,}$/;

    if (!sNombre)
        errores.push("El nombre es obligatorio");

    if (!sEmail)
        errores.push("El correo es obligatorio");
    else if (!regexEmail.test(sEmail))
        errores.push("registre un correo valido");

    if (!sUsuario)
        errores.push("El usuario es obligatorio");

    if (!iId || iId == 0)
    {
        if (!sPassword)
            errores.push("El password es obligatorio");

        else if (!regexPassword.test(sPassword))
            errores.push("La contraseña debe de tener al menos 1 letra mayuscula, 1 letra minuscula, 1 caracter especial y la longitud debe de ser de 14 digitos minimo");
    }
    else
    {
        if (sPassword && !regexPassword.test(sPassword))
            errores.push("La nueva contraseña no cumple con los requisitos, la contraseña debe de tener al menos 1 letra mayuscula, 1 letra minuscula, 1 caracter especial y la longitud debe de ser de 14 digitos minimo");
    }

    const contErrores = $('#erroresForm');

    if (errores.length > 0)
    {
        contErrores.html(errores.join('<br>')).removeClass('d-none');
        return false;
    }
    else
    {
        contErrores.addClass('d-none').html('');
        return true;
    }
}

$('#botonAgregar').click(function ()
{

    document.getElementById('formUsuario').reset();
    document.querySelector('#formUsuario input[name="iId"]').value = '';

    document.getElementById("sUsuario").readOnly = false;
    document.getElementById("dtFechaAlta").readOnly = false;
    document.getElementById("dtFechaModificacion").readOnly = false;
});

//Guardar usuario
$('#botonRegistrar').click(function () {

    if (!validarForm()) {
        return;
    }

    const formData = $('#formUsuario').serialize();

    $.ajax({
        type: 'POST',
        url: '/Usuarios/ListaUsuarios?handler=GuardarUsuario',
        data: formData,
        success: function (res) {
            const ahora = new Date();
            // yyyy-MM-ddTHH:mm para datetime-local
            const fechaActual = ahora.toISOString().substring(0, 16);

            $('#formUsuario input[name="dtFechaAlta"]').val(fechaActual);
            $('#formUsuario input[name="dtFechaModificacion"]').val(fechaActual);

            alert('Usuario guardado correctamente');
            myModal.hide();
            cargarUsuarios();
        },
        error: function (err) {
            alert(err.responseJSON.join("\n"));
            return false;
        }
    });
});

$(document).on('click','.btnEditar',function () {
    const id = $(this).data('id');
    console.log("Click editar:", id);

    //se deshabilitan campos que no se deben de mover, se deja readOnly porque con disabled no manda datos al backkk
    //const campoUsuario = document.getElementById("sUsuario");
    document.getElementById("sUsuario").readOnly = true;
    document.getElementById("dtFechaAlta").readOnly = true;
    document.getElementById("dtFechaModificacion").readOnly = true;
    //campoUsuario.disabled = true;


    $.ajax({
        url: '/Usuarios/ListaUsuarios?handler=ListaUsuariosId&iId='+id,
        type:'GET',
        success: function (usuario) {

            //Fecha y hora actual para el campo dtFechaModificacion, dtFechaAlta no se mueve porque el usuario solo sera actualizado
            const ahora = new Date();
            const año = ahora.getFullYear();
            const mes = String(ahora.getMonth() + 1).padStart(2, '0');
            const dia = String(ahora.getDate()).padStart(2, '0');
            const horas = String(ahora.getHours()).padStart(2, '0');
            const minutos = String(ahora.getMinutes()).padStart(2, '0');
            const fechaActual = `${año}-${mes}-${dia}T${horas}:${minutos}`;

            $('#formUsuario input[name="iId"]').val(usuario.iId);
            $('#formUsuario input[name="sNombre"]').val(usuario.sNombre);
            $('#formUsuario input[name="sUsuario"]').val(usuario.sUsuario);
            $('#formUsuario input[name="sPassword"]').val('');
            $('#formUsuario input[name="sEmail"]').val(usuario.sEmail);
            $('#formUsuario input[name="sEstatus"]').val(usuario.sEstatus);

            //se modifica el formato de fecha
            const fechaModificacion = usuario.dtFechaModificacion ? usuario.dtFechaModificacion.substring(0, 16) : '';
            const fechaAlta = usuario.dtFechaAlta ? usuario.dtFechaAlta.substring(0, 16) : '';

            $('#formUsuario input[name="dtFechaAlta"]').val(fechaAlta);
            $('#formUsuario input[name="dtFechaModificacion"]').val(fechaActual);
            myModal.show()
        }
    });
});