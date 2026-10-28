window.addEventListener("DOMContentLoaded", ()=>{
    const vm = new DatosAlumnoViewModel();
    ko.applyBindings(vm);

    vm.init();
});

function DatosAlumnoViewModel()
{
    const self = this;

    const hidNumControl = document.getElementById("hidNumControl");
    
    this.numControl = ko.observable(hidNumControl.value);
    this.nombre = ko.observable();
    this.carrera = ko.observable();
    this.semestre = ko.observable();
    this.grupo = ko.observable();
    this.generacion = ko.observable();
    this.fotografia = ko.observable();

    this.init = () => {
        obtenerAlumno();
    };

    const obtenerAlumno = async () => {
        const resp = await fetch("http://localhost:5000/alumnos/" + self.numControl());
        if(resp.ok){
            const alumno = await resp.json();
            console.log("alumno", alumno);
            const generacionFin = alumno.Generacion + 3;
            self.nombre(alumno.Paterno + " " + alumno.Materno + " " + alumno.Nombre);
            self.generacion(alumno.Generacion + "-" + generacionFin);
            self.carrera(alumno.Carrera);
            self.semestre(alumno.Semestre);
            self.grupo(alumno.Grupo);
        }
    }
}