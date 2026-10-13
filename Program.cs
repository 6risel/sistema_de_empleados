using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpleados
    {
        internal class Program
        {
            static int max_emp = 50;

            static string[] nombres = new string[max_emp];
            static string[] correo_emp = new string[max_emp];

            static string ruta_archivo = "empleados.txt";

            static int cant_emp = 0;

            static public void titulo()
            {
                Console.WriteLine("================================");
                Console.WriteLine("     SISTEMA DE EMPLEADOS");
                Console.WriteLine("================================");
            }

            static public void registro_empleado()
            {
                Console.WriteLine("REGISTRAR EMPLEADO");

                if (cant_emp >= max_emp)
                {
                    Console.WriteLine("Alcanzo la capacidad maxima de datos");
                    return;
                }

                Console.Write("Ingresar nombres y apellidos: ");
                string nombres_emp = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nombres_emp))
                {
                    Console.WriteLine("Error: El nombre no puede estar vacio");
                    return;
                }

                string nombres_Mayus = nombres_emp.ToUpper();

            string[] partes = nombres_Mayus.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string primer_nombre = partes[0];

                string correo = $"{primer_nombre.ToLower()}@upn.edu.pe";

                nombres[cant_emp] = nombres_Mayus;
                correo_emp[cant_emp] = correo;

                cant_emp++;

                Console.WriteLine("Empleado registrado correctamente");
                Console.WriteLine("Correo: " + correo);
            }

            static public void buscar_empleado()
            {
                Console.WriteLine("BUSQUEDA DE EMPLEADO");

                if (cant_emp == 0)
                {
                    Console.WriteLine("No hay registros de empleados");
                    return;
                }

                Console.Write("Ingresar nombre o apellido a buscar: ");
                string buscar = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(buscar))
                {
                    Console.WriteLine("Debe ingresar un nombre o apellido");
                    return;
                }

                bool encontrado = false;

                for (int i = 0; i < cant_emp; i++)
                {
                    if (nombres[i].Contains(buscar))
                    {
                        Console.WriteLine("Nombres: " + nombres[i]);
                        Console.WriteLine("Correo: " + correo_emp[i]);
                        Console.WriteLine("---------------------------");

                        encontrado = true;
                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine("No se encontraron coincidencias");
                }
            }

            static public void ordenar()
            {
                if (cant_emp <= 1)
                {
                    Console.WriteLine("No hay suficientes registros para ordenar");
                    return;
                }

                for (int i = 0; i < cant_emp - 1; i++)
                {
                    for (int j = 0; j < cant_emp - 1 - i; j++)
                    {
                        if (string.Compare(nombres[j], nombres[j + 1],
                            StringComparison.Ordinal) > 0)
                        {
                            string temp_nom = nombres[j];
                            nombres[j] = nombres[j + 1];
                            nombres[j + 1] = temp_nom;

                            string temp_correo = correo_emp[j];
                            correo_emp[j] = correo_emp[j + 1];
                            correo_emp[j + 1] = temp_correo;
                        }
                    }
                }

                Console.WriteLine("Empleados ordenados alfabeticamente (A-Z)");
            }

            static public void mostrar()
            {
                Console.WriteLine("LISTADO DE EMPLEADOS");

                if (cant_emp == 0)
                {
                    Console.WriteLine("No hay registros por mostrar");
                    return;
                }

                Console.WriteLine("Nombres y apellidos\t\tCorreo");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < cant_emp; i++)
                {
                    Console.WriteLine(nombres[i] + "\t\t" + correo_emp[i]);
                }
            }

            static public void Guardar_datos_Archivo()
            {
                Console.WriteLine("GUARDAR DATOS EN ARCHIVO");

                if (cant_emp == 0)
                {
                    Console.WriteLine("No hay registros para guardar");
                    return;
                }

                using (StreamWriter escribir = new StreamWriter(ruta_archivo))
                {
                    for (int i = 0; i < cant_emp; i++)
                    {
                        escribir.WriteLine(nombres[i] + "," + correo_emp[i]);
                    }
                }

                Console.WriteLine(cant_emp + " empleados guardados en el archivo");
            }

            static public void Leer_datos_Archivo()
            {
                Console.WriteLine("LEER DATOS DEL ARCHIVO");

                if (!File.Exists(ruta_archivo))
                {
                    Console.WriteLine("No existe el archivo de empleados");
                    return;
                }

                string[] lineas = File.ReadAllLines(ruta_archivo);
                int num_linea = 1;

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split(',');

                    if (partes.Length == 2)
                    {
                        Console.WriteLine(
                            num_linea + ". Nombres: " + partes[0] +
                            " | Correo: " + partes[1]
                        );

                        num_linea++;
                    }
                }

                if (num_linea == 1)
                {
                    Console.WriteLine("El archivo no contiene registros validos");
                }
            }

            static public void Cargar_datos_Archivo()
            {
                Console.WriteLine("CARGAR DATOS DEL ARCHIVO");

                if (!File.Exists(ruta_archivo))
                {
                    Console.WriteLine("No existe el archivo de empleados");
                    return;
                }

                string[] lineas = File.ReadAllLines(ruta_archivo);

                cant_emp = 0;

                for (int i = 0; i < lineas.Length && cant_emp < max_emp; i++)
                {
                    string[] partes = lineas[i].Split(',');

                    if (partes.Length == 2 &&
                        !string.IsNullOrWhiteSpace(partes[0]) &&
                        !string.IsNullOrWhiteSpace(partes[1]))
                    {
                        nombres[cant_emp] = partes[0];
                        correo_emp[cant_emp] = partes[1];

                        cant_emp++;
                    }
                }

                Console.WriteLine(cant_emp + " empleados cargados correctamente");
            }

            static void Main(string[] args)
            {
                int opc = 0;

                titulo();

                // Cargar los empleados guardados al iniciar el programa
                Cargar_datos_Archivo();

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("========== MENU PRINCIPAL ==========");
                    Console.WriteLine("[1] Registrar empleado");
                    Console.WriteLine("[2] Buscar empleado");
                    Console.WriteLine("[3] Ordenar alfabeticamente (A-Z)");
                    Console.WriteLine("[4] Mostrar empleados");
                    Console.WriteLine("[5] Guardar datos en archivo");
                    Console.WriteLine("[6] Leer datos del archivo");
                    Console.WriteLine("[7] Cargar empleados del archivo");
                    Console.WriteLine("[8] Salir");
                    Console.Write("Ingresar opcion: ");

                    if (!int.TryParse(Console.ReadLine(), out opc))
                    {
                        Console.WriteLine("Ingrese un valor numerico");
                        opc = 0;
                        continue;
                    }

                    switch (opc)
                    {
                        case 1:
                            registro_empleado();
                            break;

                        case 2:
                            buscar_empleado();
                            break;

                        case 3:
                            ordenar();
                            break;

                        case 4:
                            mostrar();
                            break;

                        case 5:
                            Guardar_datos_Archivo();
                            break;

                        case 6:
                            Leer_datos_Archivo();
                            break;

                        case 7:
                            Cargar_datos_Archivo();
                            break;

                        case 8:
                            Console.WriteLine("Gracias por usar el sistema");
                            break;

                        default:
                            Console.WriteLine("Opcion incorrecta. Elija del 1 al 8");
                            break;
                    }

                } while (opc != 8);
            }
        }
}
    
