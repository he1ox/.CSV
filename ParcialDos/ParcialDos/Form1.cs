using ParcialDos.clases;
using System;
using System.Windows.Forms;

namespace ParcialDos
{
    public partial class Form1 : Form
    {

        private string[] ArregloNotas;
        private string[,] ArregloUniversal;

        public Form1()
        {
            InitializeComponent();
        }

        //Carga de .CSV a la aplicacion.
        private void buttonCargarArchivo_Click(object sender, EventArgs e)
        {
            ClsArchivo ar = new ClsArchivo();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Selecciona un archivo .CSV";
            ofd.InitialDirectory = @"C:\Users\georg\source\repos\PARCIAL II\";
            ofd.Filter = "Archivo plano (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var archivo = ofd.FileName;

                //lbl muestra archivo actual
                lblMostrarArchivoActual.Text = archivo;

                string resultado = ar.leerTodoArchivo(archivo);
                ArregloNotas = ar.LeerArchivo(archivo);

                textBoxResultado.Text = resultado;
            }

        }//End Carga .CSV

        private void buttonNombres_Click(object sender, EventArgs e)
        {

            string[,] ArregloDosDimensiones = new string[ArregloNotas.Length, 6];
            int NumeroLinea = 0;

            foreach (string linea in ArregloNotas)
            {
                string[] datosUnitarios = linea.Split(';');
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.correlativo] = datosUnitarios[0]; //Se agrega el correlativo, numeroLinea = 0
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.Nombre] = datosUnitarios[1]; //Se agrega el nombre
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.ParcialUno] = datosUnitarios[2]; // Agrega nota parcial uno a la matriz
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.ParcialDos] = datosUnitarios[3]; // ....
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.ParcialTres] = datosUnitarios[4]; // ....
                ArregloDosDimensiones[NumeroLinea, EnumColumnas.Seccion] = datosUnitarios[5]; // ....

                NumeroLinea++; //para ir iterando las filas. 
            }

            ArregloUniversal = ArregloDosDimensiones;


            //Cargar datos a los listbox.
            CargarEstudiantes(ArregloDosDimensiones);


            //Cargar promedios por parcial , de manera general.
            ClsPromedios p1 = new ClsPromedios();
            lbl_general_parcial_P1_txt.Text = $"{MostrarParcial(ArregloDosDimensiones, EnumColumnas.ParcialUno, p1)} puntos.";
            ClsPromedios p2 = new ClsPromedios();
            lbl_general_parcial_P2_txt.Text = $"{MostrarParcial(ArregloDosDimensiones, EnumColumnas.ParcialDos, p2)} puntos.";
            ClsPromedios p3 = new ClsPromedios();
            lbl_general_parcial_P3_txt.Text = $"{MostrarParcial(ArregloDosDimensiones, EnumColumnas.ParcialTres, p3)} puntos.";


            //Cargar promedios por seccion, generales.
            ClsPromedios A = new ClsPromedios();
            lbl_prom_A.Text = PromedioGeneralSeccion(ArregloDosDimensiones,"A", A);
            ClsPromedios B = new ClsPromedios();
            lbl_prom_B.Text = PromedioGeneralSeccion(ArregloDosDimensiones, "B", B);
            ClsPromedios C = new ClsPromedios();
            lbl_prom_C.Text = PromedioGeneralSeccion(ArregloDosDimensiones, "C", C);
            ClsPromedios D = new ClsPromedios();
            lbl_prom_D.Text = PromedioGeneralSeccion(ArregloDosDimensiones, "D", D);

            //Despliega el mejor promedio o mas alto
            ClsPromedios mayorNota = new ClsPromedios();
            lblMejorNota.Text = $"{mayorNota.nombre_nota_mayor(ArregloDosDimensiones).ToString()} pts.";


            //Despliega los mejores promedios de x seccion
            ClsPromedios MayorA = new ClsPromedios();
            lblBestA.Text = MayorA.nombre_nota_mayor(ArregloDosDimensiones, "A");
            ClsPromedios MayorB = new ClsPromedios();
            lblBestB.Text = MayorB.nombre_nota_mayor(ArregloDosDimensiones, "B");
            ClsPromedios MayorC = new ClsPromedios();
            lblBestC.Text = MayorC.nombre_nota_mayor(ArregloDosDimensiones, "C");
            ClsPromedios MayorD = new ClsPromedios();
            lblBestD.Text = MayorD.nombre_nota_mayor(ArregloDosDimensiones, "D");


            //Nombres + suma notas
            ClsPromedios Estudiantes_Notas = new ClsPromedios();

            string[,] arrayEstudiantes = Estudiantes_Notas.Clasificar_Alumnos(ArregloDosDimensiones);

            for (int i = 1; i < ArregloNotas.Length; i++)
            {
                listboxNombre.Items.Add(arrayEstudiantes[i,0]);

                listbox_suma_notas.Items.Add(arrayEstudiantes[i, 1]);
            }


            string[] nombres = OrdenAlfabetico(ArregloDosDimensiones);


            for (int i = 1; i < nombres.Length; i++)
            {
                listBox2.Items.Add(nombres[i]);
            }

            for (int i = 1; i < nombres.Length; i++)
            {
                listBox1.Items.Add(i);
            }


        }//End ButtonNombresClick

        //Funcion para promedios
        private int promedios(string[,] matriz, int col)
        {
            int acum = 0;
            int prom = 0;
            int cantFilas = matriz.GetLength(0); //Asigna dimensiones de fila


            for (int i = 1; i < cantFilas; i++) //Comienza en 1, para evitar el encabezado.
            {
                acum += Convert.ToInt32(matriz[i, col]);
            }

            prom = acum / (cantFilas - 1);

            return prom;
        }//End promedios()

        private void CargarEstudiantes(string[,] matriz)
        {

            for (int i = 1; i < matriz.GetLength(0); i++)
            {
                listboxEstudiantes_indice.Items.Add(matriz[i, EnumColumnas.correlativo]);
                listBoxEstudiantes_nombre.Items.Add(matriz[i, EnumColumnas.Nombre]);
                listboxEstudiantes_P1.Items.Add(matriz[i, EnumColumnas.ParcialUno]);
                listboxEstudiantes_P2.Items.Add(matriz[i, EnumColumnas.ParcialDos]);
                listboxEstudiantes_P3.Items.Add(matriz[i, EnumColumnas.ParcialTres]);
                listboxEstudiantes_Seccion.Items.Add(matriz[i, EnumColumnas.Seccion]);
            }

        }

        private string MostrarParcial(string[,] matriz, int column_parcial, ClsPromedios obj)
        {
            return obj.promedios_por_parcial(matriz, column_parcial).ToString();
        }


        private string MostrarPromedio_Seccion(string[,] matriz, int column_parcial, string seccion, ClsPromedios obj)
        {
            return obj.promedios_por_seccion(matriz, column_parcial, seccion).ToString();
        }


        private string PromedioGeneralSeccion(string[,] matriz,string seccion, ClsPromedios obj)
        {
            return obj.promedios_general_seccion( matriz,seccion).ToString();
        }


        private void lblMostrarArchivoActual_Click(object sender, EventArgs e)
        {

        }

        //BOTONES PARA MOSTRA PROMEDIOS POR SECCION
        private void btn_prom_porSeccion_A_Click(object sender, EventArgs e)
        {
            ClsPromedios p1A = new ClsPromedios();
            ClsPromedios p2A = new ClsPromedios();
            ClsPromedios p3A = new ClsPromedios();

            lbl_prom_seccion_p1.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialUno, "A", p1A)} puntos.";
            lbl_prom_seccion_p2.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialDos, "A", p2A)} puntos.";
            lbl_prom_seccion_p3.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialTres, "A", p3A)} puntos.";

        }

        private void btn_prom_porSeccion_B_Click(object sender, EventArgs e)
        {
            ClsPromedios p1B = new ClsPromedios();
            ClsPromedios p2B = new ClsPromedios();
            ClsPromedios p3B = new ClsPromedios();


            lbl_prom_seccion_p1.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialUno, "B", p1B)} puntos.";
            lbl_prom_seccion_p2.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialDos, "B", p2B)} puntos.";
            lbl_prom_seccion_p3.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialTres, "B", p3B)} puntos.";


        }

        private void btn_prom_porSeccion_C_Click(object sender, EventArgs e)
        {
            ClsPromedios p1C = new ClsPromedios();
            ClsPromedios p2C = new ClsPromedios();
            ClsPromedios p3C = new ClsPromedios();

            lbl_prom_seccion_p1.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialUno, "C", p1C)} puntos.";
            lbl_prom_seccion_p2.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialDos, "C", p2C)} puntos.";
            lbl_prom_seccion_p3.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialTres, "C", p3C)} puntos.";
        }

        private void btn_prom_porSeccion_D_Click(object sender, EventArgs e)
        {
            ClsPromedios p1D = new ClsPromedios();
            ClsPromedios p2D = new ClsPromedios();
            ClsPromedios p3D = new ClsPromedios();


            lbl_prom_seccion_p1.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialUno, "D", p1D)} puntos.";
            lbl_prom_seccion_p2.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialDos, "D", p2D)} puntos.";
            lbl_prom_seccion_p3.Text = $"{MostrarPromedio_Seccion(ArregloUniversal, EnumColumnas.ParcialTres, "D", p3D)} puntos.";

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        //End Botones promedios por seccion


        private string[] OrdenAlfabetico(string[,] matriz)
        {
            string[] nombres = new string[matriz.GetLength(0)];
            string temp;

            for (int i = 1; i < matriz.GetLength(0); i++)
            {
                nombres[i] = matriz[i, EnumColumnas.Nombre];
            }

            for (int i = 1; i < nombres.Length; i++)
            {
                for (int j = i + 1; j < nombres.Length; j++)
                {
                    if (String.Compare(nombres[i], nombres[j]) > 0)
                    {
                        temp = nombres[i];
                        nombres[i] = nombres[j];
                        nombres[j] = temp;
                    }
                }
            }
            return  nombres;

        } 


    }

}
