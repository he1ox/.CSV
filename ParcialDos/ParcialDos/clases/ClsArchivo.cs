using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDos
{
    class ClsArchivo
    {
        public string[] LeerArchivo(string archivo)
        {
            //File.ReadAllLines devuelve un array de string, cada linea es parte del array.
            String[] lineas = File.ReadAllLines(archivo, Encoding.Default);
            return lineas;
        }


        public string leerTodoArchivo(string archivo)
        {
            string contenidoArchivo;
            using (StreamReader reader = new StreamReader(archivo, Encoding.Default))
            {
                //Caracter por caracter, desde la posicion actual hasta la ultima.
                contenidoArchivo = reader.ReadToEnd();
            }
            return contenidoArchivo;
        }


    }
}
