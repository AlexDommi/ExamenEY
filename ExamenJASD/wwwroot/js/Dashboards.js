
const ctx = document.getElementById('graficaUsuarios');

new Chart(ctx, {
    type: 'bar', // puedes cambiar a 'line', 'pie', etc.
data: {
    labels: ['Total Usuarios', 'Usuarios Activos', 'Usuarios Inactivos'],
datasets: [{
    label: 'Usuarios',
    data: [totalUsuarios, usuariosActivos, usuariosInactivos],
borderWidth: 1
        }]
    }
});