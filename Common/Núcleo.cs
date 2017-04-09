/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 08:24 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
//using System
using System.Collections;
using System.Collections.Generic;

namespace MemoryEmulator.Common
{
	/// <summary>
	/// Description of Núcleo.
	/// </summary>
	public class Núcleo
	{
		ArrayList Cola;
		public Núcleo()
		{
			// Algo jejeje
		}
		
		enum TipoAlgoritmo
		{
			PrimeroEntrarPrimeroSalir = 1,
			TurnoRotatorio,
			PrimeroMásCorto,
			MenorTiempoRestante,
			MayorTasaRespuesta,
			UsadoHaceMásTiempo
		}
		
		public class ListaProcesos
		{
			public ListaProcesos (string nombre, double tamano, double tiempo, double entrada)
			{
				this.Nombre = nombre;
				this.Tamano = tamano;
				this.Tiempo = tiempo;
				this.Entrada = entrada;
			}
				
			public string Nombre { get; private set; }
			public double Tamano { get; private set; }
			public double Tiempo { get; private set; }
			public double Entrada{ get; private set; }
		}
		
		public class ObjetoMemoria
		{
			public ObjetoMemoria (string nombre, double tamano, double tiempo, double entrada, int orden, double tasares, int atendido)
			{
				this.Nombre   = nombre;
				this.Tamano   = tamano;
				this.Tiempo   = tiempo;
				this.Entrada  = entrada;
				this.Orden    = orden;
				this.TasaRes  = tasares;
				this.Atendido = atendido;
			}
				
			public string Nombre   { get; private set; }
			public double Tamano   { get; private set; }
			public double Tiempo   { get; set; }
			public double Entrada  { get; private set; }
			public int	  Orden    { get; set; }
			public double TasaRes  { get; set; }
			public int    Atendido { get; set; }
		}
		
		public void Procesar(ArrayList procesos, uint algoritmoEntrada, uint algoritmoSalida, double tmemoria, double quantum, out string registro)
		{
			string tempreg = String.Empty;
			ArrayList ColaTemporal = new ArrayList();
			ColaTemporal.Clear();
			// Almacenamos la lista de procesos recibidos en la Cola
			for (int ciclo = 0; ciclo < procesos.Count; ciclo++)
			{
				Cola.Add(new ObjetoMemoria(((ListaProcesos)procesos[ciclo]).Nombre,((ListaProcesos)procesos[ciclo]).Tamano,((ListaProcesos)procesos[ciclo]).Tiempo,((ListaProcesos)procesos[ciclo]).Entrada,ciclo + 1,0,0));
			}
			
			// Depuramos la lista de procesos para eliminar los procesos que sean mayores a la capacidad de la memoria
			ColaTemporal = DepurarProcesos(Cola,tmemoria,out tempreg);
			Cola.Clear();
			Cola = ColaTemporal;
			ColaTemporal.Clear();
			
			// Fijamos la tasa de respuesta de todos los procesos en la cola
			ColaTemporal = TasaRespuesta(Cola, quantum);
			Cola.Clear();
			Cola = ColaTemporal;
			ColaTemporal.Clear();
			
			// Determinamos que algoritmos utilizaremos
			if ((TipoAlgoritmo)algoritmoEntrada == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
				tempreg += PrimeroEntrarPrimeroSalir(Cola, algoritmoSalida, tmemoria, quantum);
			if ((TipoAlgoritmo)algoritmoEntrada == TipoAlgoritmo.TurnoRotatorio)
				tempreg += TurnoRotatorio(Cola, algoritmoSalida, tmemoria, quantum);
			if ((TipoAlgoritmo)algoritmoEntrada == TipoAlgoritmo.PrimeroMásCorto)
				tempreg += PrimeroMásCorto(Cola, algoritmoSalida, tmemoria, quantum);
			if ((TipoAlgoritmo)algoritmoEntrada == TipoAlgoritmo.MenorTiempoRestante)
				tempreg += MenorTiempoRestante(Cola, algoritmoSalida, tmemoria, quantum);
			if ((TipoAlgoritmo)algoritmoEntrada == TipoAlgoritmo.MayorTasaRespuesta)
				tempreg += MayorTasaRespuesta(Cola, algoritmoSalida, tmemoria, quantum);
			
			// Asignamos el registro obtenido del algoritmo a la variable de salida de registro principal
			registro = tempreg;
		}
		
		private ArrayList TasaRespuesta(ArrayList ColaProcesos, double quantum)
		{
			double tasaResTemp = 0;
			for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
			{
				tasaResTemp = (((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo + quantum) / ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
				((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes = tasaResTemp;
			}
			
			return ColaProcesos;
		}
		
		private ArrayList DepurarProcesos(ArrayList ColaProcesos, double tmemoria, out string registro)
		{
			int pos = 0;
			bool terminado = false;
			registro = String.Empty;
			while (!terminado)
			{
				if (pos == ColaProcesos.Count)
					terminado = true;
				
				if (((ObjetoMemoria)ColaProcesos[pos]).Tamano > tmemoria)
				{
					registro += "Se ha eliminado de la lista de procesos el proceso \"" + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + "\" porque era más grande que la capacidad de la memoria." + Environment.NewLine;
					ColaProcesos.RemoveAt(pos);
				}
				else
					pos++;
			}
			
			return ColaProcesos;
		}
		
		private string ListarColaProcesos(ArrayList ColaProcesos)
		{
			string tempreg = String.Empty;
			
			tempreg += "==================================================" + Environment.NewLine;
			tempreg += "Cola: ";
			foreach (object Cp in ColaProcesos)
			{
				tempreg += ((ObjetoMemoria)Cp).Nombre + ", ";
			}
			tempreg = tempreg.Substring(0,tempreg.Length - 2);
			tempreg += Environment.NewLine;
			
			return tempreg;
		}
		
		private string PrimeroEntrarPrimeroSalir(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			int pos = 0;
			bool encontrado = false;
			
			// Determinamos el algoritmo de salida a utilizar
			if ((TipoAlgoritmo)algoritmoSalida == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
			{
				while (!Terminado)
				{
					// Determinamos cuanto tiempo hay pendiente entre todos los procesos
					for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
						Tiempo += ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
					for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
						Tiempo += ((ObjetoMemoria)Memoria[ciclo]).Tiempo;
					
					// Determinamos si se ha terminado de atender todos los procesos
					if ((ColaProcesos.Count == 0) && (Memoria.Count == 0) && (Tiempo == 0))
						Terminado = true;
					
					if (!Terminado)
					{
						if (ColaProcesos.Count > 0)
						{
							// Aún hay procesos en memoria, se irán ingresando a memoria y regresando a la cola hasta que
							// no queden más procesos en la cola.
							
							// Si el proceso que en la primera posición de la cola cabe en el espacio disponible en memoria,
							// lo agregamos a esta y lo procesamos.
							if (((ObjetoMemoria)ColaProcesos[0]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[0]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[0]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[0]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(0);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count + 1;
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									TiempoAcumulado += quantum;
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length));
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								// Acá se aplica el algoritmo de salida, primero en entrar, primero en salir
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									// Buscamos el primer proceso que entró a memoria
									if (((ObjetoMemoria)Memoria[ciclo]).Orden == 1)
									{
										Registro += ListarColaProcesos(ColaProcesos);
										// Lo sacamos de la memoria y lo movemos a la cola
										ColaProcesos.Add(Memoria[ciclo]);
										Registro += "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
									}
									
									if (encontrado)
										((ObjetoMemoria)Memoria[ciclo]).Orden -= 1;
										
								}
							}
						}
						else
						{
							// Ya no hay procesos en cola, solo hay que iterar todos los existentes en memoria hasta que
							// todos se hayan completado.
							pos = 0;
							while (Memoria.Count > 0)
							{
								Registro += ListarColaProcesos(ColaProcesos);
								((ObjetoMemoria)Memoria[pos]).Atendido++;
								if ((((ObjetoMemoria)Memoria[pos]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									TiempoAcumulado += quantum;
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Memoria.RemoveAt(pos);
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
								}
								
								if ((pos + 1) <= (Memoria.Count - 1))
									pos++;
								else
									pos = 0;
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
			}
			
			return Registro;
		}
		
		private string TurnoRotatorio(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			
			// Determinamos el algoritmo de salida a utilizar
			if ((TipoAlgoritmo)algoritmoSalida == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
			{
				
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
			}
			
			return Registro;
		}
		
		private string PrimeroMásCorto(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			
			// Determinamos el algoritmo de salida a utilizar
			if ((TipoAlgoritmo)algoritmoSalida == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
			{
				
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
			}
			
			return Registro;
		}
		
		private string MenorTiempoRestante(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			
			// Determinamos el algoritmo de salida a utilizar
			if ((TipoAlgoritmo)algoritmoSalida == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
			{
				
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
			}
			
			return Registro;
		}
		
		private string MayorTasaRespuesta(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			
			// Determinamos el algoritmo de salida a utilizar
			if ((TipoAlgoritmo)algoritmoSalida == TipoAlgoritmo.PrimeroEntrarPrimeroSalir)
			{
				
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
			}
			
			return Registro;
		}
	}
}
