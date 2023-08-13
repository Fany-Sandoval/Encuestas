$(document).ready(function () {
    $('#UsuariosId').hide();

    $('#RolesId').change(function () {
        let id = $('#RolesId').val();
        $('#UsuariosId').show();
    });
});