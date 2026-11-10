window.addEventListener("DOMContentLoaded", ()=>{
    const vm = new DatosAlumnoViewModel();
    ko.applyBindings(vm);

    vm.init();
});

function DatosAlumnoViewModel()
{
    const self = this;

    const hidNumControl = document.getElementById("hidNumControl");
    const archivo = document.getElementById("archivo");

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

    archivo.addEventListener("change", self.cambiarFoto);

    this.eliminarFoto = async () => {
        const url = "http://localhost:5000/alumnos/" + self.numControl() + "/foto";
        const resp = await fetch(url, {
            method: "DELETE"
        });

        if(resp.ok){
            self.fotografia("");
            self.fotografia("/imagenes/alumno.jpg"); 
        }
    };
    
    this.abrirSelectorFoto = () => {
        archivo.click();
    };

    this.cambiarFoto = async () => {
        if(!archivo.value){
            return;
        }
        const formData = new FormData();
        formData.append("archivo", archivo.files[0]);
        const url = "http://localhost:5000/alumnos/" + self.numControl() + "/foto";
        const resp = await fetch(url, {
            method: "PATCH",
            body : formData 
        });

        if(resp.ok){
            const urlFoto = await resp.text();
            const separador = urlFoto.includes("?") ? "&" : "?";
            self.fotografia(urlFoto + separador + "t=" + Date.now());
        }

        archivo.value = "";
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
            if(alumno.Foto){
                self.fotografia(alumno.Foto);
            }
            else{
                self.fotografia("/imagenes/alumno.jpg"); 
            }
        }
    };
}