window.addEventListener("DOMContentLoaded", ()=>{
    const vm = new ListadoAlumnosViewModel();
    ko.applyBindings(vm);

    vm.init();
});

function ListadoAlumnosViewModel()
{
    const self = this;

    this.registros = ko.observableArray([]);

    const obtenerAlumnos = async () => {
        const respuesta = await fetch("http://localhost:5000/alumnos");

        if(respuesta.ok){
            const datos = await respuesta.json();
            const registros = datos.map(alumno => {
                return {
                    ...alumno,
                    NombreCompleto: alumno.Paterno + " " + alumno.Materno + " " + alumno.Nombre,
                    SemestreGrupo: alumno.Semestre + " " + alumno.Grupo
                }
            });
            
            self.registros(registros);
        }
    };

    this.init = () => {
        obtenerAlumnos();
    };
}