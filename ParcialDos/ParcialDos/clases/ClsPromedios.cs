using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialDos.clases
{
     class ClsPromedios : InterfacePromedios 
    {
        
        public string[,] Clasificar_Alumnos(string[,] matriz)
        {
            string[,] temp = new string[matriz.GetLength(0), 2];
            int sumatoria = 0;


            for (int i = 1; i < matriz.GetLength(0); i++)
            {
                sumatoria = Convert.ToInt32(matriz[i, EnumColumnas.ParcialUno]) +
                            Convert.ToInt32(matriz[i, EnumColumnas.ParcialDos]) +
                            Convert.ToInt32(matriz[i, EnumColumnas.ParcialTres]);

                temp[i, 0] = matriz[i, EnumColumnas.Nombre];

                temp[i, 1] = sumatoria.ToString();

            }

            return temp;
        }

        public string nombre_nota_mayor(string[,] matriz)
        {
            double[] notasProm = new double[matriz.GetLength(0)];
            double temp;
            double sumatorias;

            for (int i = 1; i < matriz.GetLength(0); i++)
            {
                sumatorias = Convert.ToInt32(matriz[i, EnumColumnas.ParcialUno]) +
                            Convert.ToInt32(matriz[i, EnumColumnas.ParcialDos]) +
                            Convert.ToInt32(matriz[i, EnumColumnas.ParcialTres]);
                notasProm[i-1] = sumatorias / 3;

            }

            for (int i = 0; i < notasProm.Length; i++)
            {
                for (int j = i + 1; j < notasProm.Length; j++)
                {
                    if (notasProm[i] > notasProm[j])
                    {
                        temp = notasProm[i];
                        notasProm[i] = notasProm[j];
                        notasProm[j] = temp;
                    }
                }
            }

            temp = notasProm[notasProm.Length - 1];

            return Convert.ToString(Math.Round(temp, 2)); 
        }

        

        public int promedios_general_seccion(string[,] matriz, string seccion)
        {
            int prom; int acumEstudiante = 0; int acumCant_Estudiantes = 0; int acumEstudiante2 = 0;

            for (int i = 1; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, 5] == seccion)
                {
                    acumEstudiante = Convert.ToInt32(matriz[i, EnumColumnas.ParcialUno])+
                                     Convert.ToInt32(matriz[i,EnumColumnas.ParcialDos])+
                                     Convert.ToInt32(matriz[i, EnumColumnas.ParcialTres]);

                    acumEstudiante2 = acumEstudiante + acumEstudiante2;
                    acumCant_Estudiantes++;
                }

            }
                return prom = acumEstudiante2 / acumCant_Estudiantes;
        }


        public int promedios_por_parcial(string[,] matriz, int column_parcial)
        {
            int acum = 0;
            int prom = 0;
            int cantFilas = matriz.GetLength(0); //Asigna dimensiones de fila


            for (int i = 1; i < cantFilas; i++) //Comienza en 1, para evitar el encabezado.
            {
                acum += Convert.ToInt32(matriz[i, column_parcial]);
            }

            prom = acum / (cantFilas - 1);

            return prom;
        }

        public int promedios_por_seccion(string[,] matriz, int column_parcial, string seccion)
        {
            int acum = 0; // Acumula la suma de la columna especificada
            int acumEstudiantes = 0; //Acumula la cantidad de estudiantes de la seccion x
            int promedio = 0;
            int cantFilas = matriz.GetLength(0);


            for (int i = 1; i < cantFilas; i++) //Comienza en 1, para evitar el encabezado.
            {
                if (matriz[i, 5] == seccion) //Busca fila x fila, en la columna 5 por incidencias con la seccion x.
                {
                    acum += Convert.ToInt32(matriz[i, column_parcial]);

                    acumEstudiantes++;
                }
            }

            promedio = acum / acumEstudiantes;

            return promedio;
        }

        public string nombre_nota_mayor(string[,] matriz, string seccion)
        {
            int acum = 0;
            double mayor = 0, centinela = 0;
            double sumatoria = 0;
            double promEstudiante = 0;
            double[] SumatoriaProm = new double[matriz.GetLength(0)];

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, EnumColumnas.Seccion] == seccion)
                {
                    sumatoria = (Convert.ToInt32(matriz[i, EnumColumnas.ParcialUno]) +
                                    Convert.ToInt32(matriz[i, EnumColumnas.ParcialDos]) +
                                    Convert.ToInt32(matriz[i, EnumColumnas.ParcialTres])) / 3;
                    promEstudiante = sumatoria;
                    if (acum != 0)
                    {
                        SumatoriaProm[acum] = promEstudiante;
                        if (SumatoriaProm[acum] > centinela)
                        {
                            mayor = SumatoriaProm[acum]; centinela = SumatoriaProm[acum];
                        }
                    }
                }
                acum++;
            }
            return Convert.ToString(Math.Round(mayor, 2));
        }


    }
}
