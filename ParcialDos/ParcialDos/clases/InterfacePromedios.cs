using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDos.clases
{
     interface InterfacePromedios
    {
        /// <summary>
        /// Retorna el promedio en base a una col. especifica
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="column_parcial"></param>
        /// <returns></returns>
        int promedios_por_parcial(string[,] matriz, int column_parcial);


        /// <summary>
        /// Retorna promedio de un parcial por seccion
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="column_parcial"></param>
        /// <returns></returns>
        int promedios_por_seccion(string[,] matriz, int column_parcial, string seccion);



        /// <summary>
        /// Retorna el promedio general de todos los alumnos por seccion
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="column_parcial"></param>
        /// <param name="seccion"></param>
        /// <returns></returns>
        int promedios_general_seccion(string[,] matriz, string seccion);





        /// <summary>
        /// Retorna 2 columnas
        /// {nombre,suma de los 3 parciales}
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="seccion"></param>
        /// <returns></returns>
        string[,] Clasificar_Alumnos(string[,] matriz);




        /// <summary>
        /// retorna la mejor nota independiente de la seccion
        /// </summary>
        /// <param name="matriz"></param>
        /// <returns></returns>
        string nombre_nota_mayor(string[,] matriz);





        /// <summary>
        /// retorna la mejor nota por seccion
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="seccion"></param>
        /// <returns></returns>
        string nombre_nota_mayor(string[,] matriz, string seccion);



    }
}
