using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InicioSesion.Models;
using InicioSesion.Repositories;
using InicioSesion.Views;

namespace InicioSesion.ViewModels
{
    public class IniciarSesionViewModel : INotifyPropertyChanged
    {
        UsuarioRepository userRepo = new UsuarioRepository();

        BienvenidaView BienvenidaView;

        AgregarView AgregarView;



        public IniciarSesionViewModel()
        {
            IniciarSesionCommand = new RelayCommand(IniciarSesion);
            VerAgregarCommand = new RelayCommand(VerAgregar);
            AgregarCommand = new RelayCommand(Agregar);
            Usuarios = new ObservableCollection<Usuario>(userRepo.GetAllUsuarios());
        }

        

        private void Agregar()
        {
            try
            {
                if (Validar(nombre, correo, direccion, password))
                {
                    userRepo.AgregarUsuario(correo, password, nombre, direccion);
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        private void VerAgregar()
        {
            AgregarView = new AgregarView();
            AgregarView.ShowDialog();
        }

        private void IniciarSesion()
        {
            try
            {
                if (Validar(correo, password))
                {
                    var result = userRepo.IniciarSesion(correo, password);

                    if (result)
                    {
                        //Abre la ventana de bienvenida.
                        BienvenidaView = new BienvenidaView();
                        BienvenidaView.ShowDialog();
                    }
                    else
                    {
                        Error = "Datos incorrectos.";
                    }
                }
                // Validar que el correo y el password contengan informacion
                //Si el resultado es verdadero, inicia sesion y se abre la nueva vista.
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }


        }

        public bool Validar(string correo, string password)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentNullException("El correo no debe de ir vacio.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("La contraseña no debe de ir vacia.");
            return true;
        }

        public bool Validar(string nombre, string correo, string direccion, string password)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentNullException("El correo no debe de ir vacio.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("La contraseña no debe de ir vacia.");
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentNullException("Por favor introduce la direccion.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("Por favor escribe el nombre.");

            return true;
        }




        //Comando, una propiedad para el correo y otra para el password


        public ICommand IniciarSesionCommand { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }

        private string correo;

        public string Correo
        {
            get { return correo; }
            set { 
                correo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Correo)));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));

            }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { 
                nombre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));

            }
        }

        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Direccion)));

            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { 
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }


        private ObservableCollection<Usuario> usuarios;

        public ObservableCollection<Usuario> Usuarios
        {
            get { return usuarios; }
            set { 
                usuarios = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Usuarios)));

            }
        }























        public event PropertyChangedEventHandler PropertyChanged;
    }
}
