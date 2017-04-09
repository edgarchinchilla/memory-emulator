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

namespace MemoryEmulator
{
	/// <summary>
	/// Description of Núcleo.
	/// </summary>
	public class Núcleo
	{
		ArrayList Cola =  new ArrayList();
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
		
		/*
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
		*/
		
		public class ObjetoMemoria
		{
			public ObjetoMemoria (string nombre, double tamano, double tiempo, double entrada, int orden, double tasares, int atendido, bool disponible)
			{
				this.Nombre   = nombre;
				this.Tamano   = tamano;
				this.Tiempo   = tiempo;
				this.Entrada  = entrada;
				this.Orden    = orden;
				this.TasaRes  = tasares;
				this.Atendido = atendido;
				this.Disponible = disponible;
			}
				
			public string Nombre     { get; private set; }
			public double Tamano     { get; private set; }
			public double Tiempo     { get; set; }
			public double Entrada    { get; private set; }
			public int	  Orden      { get; set; }
			public double TasaRes    { get; set; }
			public int    Atendido   { get; set; }
			public bool   Disponible { get; set; }
		}
		
		public void Procesar(ArrayList procesos, uint algoritmoEntrada, uint algoritmoSalida, double tmemoria, double quantum, out string registro)
		{
			string tempreg = String.Empty;
			ArrayList ColaTemporal = new ArrayList();
			ColaTemporal.Clear();
			// Almacenamos la lista de procesos recibidos en la Cola
			for (int ciclo = 0; ciclo < procesos.Count; ciclo++)
			{
				Cola.Add(new ObjetoMemoria(((FormProcesos.ListaProcesos)procesos[ciclo]).Nombre,((FormProcesos.ListaProcesos)procesos[ciclo]).Tamano,((FormProcesos.ListaProcesos)procesos[ciclo]).Tiempo,((FormProcesos.ListaProcesos)procesos[ciclo]).Entrada,ciclo + 1,0,0,false));
			}
			
			// Depuramos la lista de procesos para eliminar los procesos que sean mayores a la capacidad de la memoria
			ColaTemporal.AddRange(DepurarProcesos(Cola,tmemoria,out tempreg));
			Cola.Clear();
			Cola.AddRange(ColaTemporal);
			ColaTemporal.Clear();
			
			// Fijamos la tasa de respuesta de todos los procesos en la cola
			ColaTemporal.AddRange(TasaRespuesta(Cola, quantum));
			Cola.Clear();
			Cola.AddRange(ColaTemporal);
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
				
				if (!terminado)
				{
					if (((ObjetoMemoria)ColaProcesos[pos]).Tamano > tmemoria)
					{
						registro += "Se ha eliminado de la lista de procesos el proceso \"" + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + "\" porque era más grande que la capacidad de la memoria." + Environment.NewLine;
						ColaProcesos.RemoveAt(pos);
					}
					else
						pos++;
				}
			}
			
			return ColaProcesos;
		}
		
		private string ListarColaProcesos(ArrayList ColaProcesos)
		{
			string tempreg = String.Empty;
			int contador = 0;
			
			tempreg += "==================================================" + Environment.NewLine;
			tempreg += "Cola: ";
			foreach (object Cp in ColaProcesos)
			{
				tempreg += ((ObjetoMemoria)Cp).Nombre + ", ";
				contador++;
			}
			
			if (contador > 0)
				tempreg = tempreg.Substring(0,tempreg.Length - 2);
			else
				tempreg += "---";
			
			tempreg += Environment.NewLine;
			
			return tempreg;
		}
		
		private ArrayList ActualizarDisponibilidad(ArrayList ColaProcesos, double TiempoAcumulado)
		{
			// En caso de no haberse atendido ningún proceso aún, el tiempo acumulado debiese ser 0,
			// en dicho caso, habilitar el primer proceso como disponible.
			if (TiempoAcumulado == 0)
				((ObjetoMemoria)ColaProcesos[0]).Disponible = true;
			
			for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
			{
				if (((ObjetoMemoria)ColaProcesos[ciclo]).Entrada <= TiempoAcumulado)
					((ObjetoMemoria)ColaProcesos[ciclo]).Disponible = true;
			}
			
			return ColaProcesos;
		}
		
		private string PrimeroEntrarPrimeroSalir(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			double ValorAnterior = 0;
			int pos = 0;
			bool encontrado = false;
			bool incrementar = true;
			
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
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
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
										Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
										continue;
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
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
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								pos = 0;
								ValorAnterior = 0;
								// Acá se aplica el algoritmo de salida, usado hace más tiempo
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
									}
									else
									{
										// El proceso en esta posición ha sido atendido menos veces que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a abandonar la memoria
										if (((ObjetoMemoria)Memoria[ciclo]).Atendido < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
										}
									}	
								}
								
								Registro += ListarColaProcesos(ColaProcesos);
								// Lo sacamos de la memoria y lo movemos a la cola
								ColaProcesos.Add(Memoria[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								tmemuso -= ((ObjetoMemoria)Memoria[pos]).Tamano;
								Memoria.RemoveAt(pos);
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			
			return Registro;
		}
		
		private string TurnoRotatorio(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			ArrayList ColaTemporal = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			double ValorAnterior = 0;
			int pos = 0;
			bool encontrado = false;
			bool incrementar = true;
			
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
							
							// Actualizamos el valor de disponibilidad de todos los procesos en cola
							ColaTemporal.AddRange(ActualizarDisponibilidad(ColaProcesos,TiempoAcumulado));
							ColaProcesos.Clear();
							ColaProcesos.AddRange(ColaTemporal);
							ColaTemporal.Clear();
							
							// Buscamos el primer proceso disponible para agregarlo a la memoria
							pos = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
								if (((ObjetoMemoria)ColaProcesos[ciclo]).Disponible)
								{
									pos = ciclo;
									break;
								}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
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
										Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
										continue;
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
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
							
							// Actualizamos el valor de disponibilidad de todos los procesos en cola
							ColaTemporal.AddRange(ActualizarDisponibilidad(ColaProcesos,TiempoAcumulado));
							ColaProcesos.Clear();
							ColaProcesos.AddRange(ColaTemporal);
							ColaTemporal.Clear();
							
							// Buscamos el primer proceso disponible para agregarlo a la memoria
							pos = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
								if (((ObjetoMemoria)ColaProcesos[ciclo]).Disponible)
								{
									pos = ciclo;
									break;
								}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								pos = 0;
								ValorAnterior = 0;
								// Acá se aplica el algoritmo de salida, usado hace más tiempo
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
									}
									else
									{
										// El proceso en esta posición ha sido atendido menos veces que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a abandonar la memoria
										if (((ObjetoMemoria)Memoria[ciclo]).Atendido < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
										}
									}	
								}
								
								Registro += ListarColaProcesos(ColaProcesos);
								// Lo sacamos de la memoria y lo movemos a la cola
								ColaProcesos.Add(Memoria[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								tmemuso -= ((ObjetoMemoria)Memoria[pos]).Tamano;
								Memoria.RemoveAt(pos);
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			
			return Registro;
		}
		
		private string PrimeroMásCorto(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			double ValorAnterior = 0;
			int pos = 0;
			bool encontrado = false;
			bool incrementar = true;
			
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
							
							// Buscamos el proceso más pequeño para agregarlo a la memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tamano;
									}
									else
									{
										// El proceso en esta posición es más pequeño que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).Tamano < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tamano;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
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
										Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
										continue;
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
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
							
							// Buscamos el proceso más pequeño para agregarlo a la memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tamano;
									}
									else
									{
										// El proceso en esta posición es más pequeño que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).Tamano < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tamano;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								pos = 0;
								ValorAnterior = 0;
								// Acá se aplica el algoritmo de salida, usado hace más tiempo
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
									}
									else
									{
										// El proceso en esta posición ha sido atendido menos veces que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a abandonar la memoria
										if (((ObjetoMemoria)Memoria[ciclo]).Atendido < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
										}
									}	
								}
								
								Registro += ListarColaProcesos(ColaProcesos);
								// Lo sacamos de la memoria y lo movemos a la cola
								ColaProcesos.Add(Memoria[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								tmemuso -= ((ObjetoMemoria)Memoria[pos]).Tamano;
								Memoria.RemoveAt(pos);
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			
			return Registro;
		}
		
		private string MenorTiempoRestante(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			double ValorAnterior = 0;
			int pos = 0;
			bool encontrado = false;
			bool incrementar = true;
			
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
							
							// Buscamos el proceso con menor tiempo restante para agregarlo a la memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
									}
									else
									{
										// El proceso en esta posición es el que tiene menor tiempo restante de los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
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
										Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
										continue;
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
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
							
							// Buscamos el proceso con menor tiempo restante para agregarlo a la memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
									}
									else
									{
										// El proceso en esta posición es el que tiene menor tiempo restante de los anteriores
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).Tiempo;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								pos = 0;
								ValorAnterior = 0;
								// Acá se aplica el algoritmo de salida, usado hace más tiempo
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
									}
									else
									{
										// El proceso en esta posición ha sido atendido menos veces que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a abandonar la memoria
										if (((ObjetoMemoria)Memoria[ciclo]).Atendido < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
										}
									}	
								}
								
								Registro += ListarColaProcesos(ColaProcesos);
								// Lo sacamos de la memoria y lo movemos a la cola
								ColaProcesos.Add(Memoria[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								tmemuso -= ((ObjetoMemoria)Memoria[pos]).Tamano;
								Memoria.RemoveAt(pos);
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			
			return Registro;
		}
		
		private string MayorTasaRespuesta(ArrayList ColaProcesos, uint algoritmoSalida, double tmemoria, double quantum)
		{
			string Registro = String.Empty;
			ArrayList Memoria = new ArrayList();
			ArrayList ColaTemporal = new ArrayList();
			bool Terminado = false;
			double Tiempo = 0;
			double TiempoAcumulado = 0;
			double tmemuso = 0;
			double ValorAnterior = 0;
			int pos = 0;
			bool encontrado = false;
			bool incrementar = true;
			
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
							
							// Actualizamos el valor de la tasa de respuesta de todos los procesos en cola
							ColaTemporal.AddRange(TasaRespuesta(ColaProcesos, quantum));
							ColaProcesos.Clear();
							ColaProcesos.AddRange(ColaTemporal);
							ColaTemporal.Clear();
							
							// Buscamos el proceso con la mayor tasa de respuesta para agregarlo a la memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes;
									}
									else
									{
										// El proceso en esta posición es el que tiene la mayor tasa de respuesta de los anteriores
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes > ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
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
										Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[ciclo]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[ciclo]).Tamano;
										Memoria.RemoveAt(ciclo);
										// Reducimos el valor del ciclo para que vuelva a analizar éste número de
										// posición, donde ahora estará el proceso que estaba adelante.
										ciclo--;
										encontrado = true;
										continue;
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			else
			{
				// Esto quiere decir que el algoritmo de "Salida" a utilizar es el "Usado hace más tiempo"
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
							
							// Actualizamos el valor de la tasa de respuesta de todos los procesos en cola
							ColaTemporal.AddRange(TasaRespuesta(ColaProcesos, quantum));
							ColaProcesos.Clear();
							ColaProcesos.AddRange(ColaTemporal);
							ColaTemporal.Clear();
							
							// Buscamos el proceso que tenga la mayor tasa de respuesta para agregarlo a memoria
							pos = 0;
							ValorAnterior = 0;
							for (int ciclo = 0; ciclo < ColaProcesos.Count; ciclo++)
							{
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes;
									}
									else
									{
										// El proceso en esta posición es el que tiene la mayor tasa de respuesta de los anteriores
										// tomamos entonces los valores del nuevo proceso candidato a ingresar a memoria.
										if (((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes > ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)ColaProcesos[ciclo]).TasaRes;
										}
									}
							}
							
							if (((ObjetoMemoria)ColaProcesos[pos]).Tamano <= (tmemoria - tmemuso))
							{
								Registro += ListarColaProcesos(ColaProcesos);
								Memoria.Add(ColaProcesos[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Entra -> " + ((ObjetoMemoria)ColaProcesos[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)ColaProcesos[pos]).Tiempo + Environment.NewLine;
								ColaProcesos.RemoveAt(pos);
								((ObjetoMemoria)Memoria[Memoria.Count - 1]).Orden = Memoria.Count;
								tmemuso += ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
								
								if ((((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo - quantum) < 0)
								{
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									//TiempoAcumulado += quantum;
									//TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo.ToString().Length - 1));
									TiempoAcumulado += quantum - (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo * -1);
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo = 0;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
									Memoria.RemoveAt(Memoria.Count -1);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo -= quantum;
									((ObjetoMemoria)Memoria[Memoria.Count - 1]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Nombre + Environment.NewLine;
										tmemuso -= ((ObjetoMemoria)Memoria[Memoria.Count - 1]).Tamano;
										Memoria.RemoveAt(Memoria.Count -1);
									}
								}
								
								Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
							}
							else
							{
								// Hay procesos en cola, pero no caben en memoria, entonces moveremos de la memoria a la
								// cola un proceso, según el algoritmo que corresponda;
								
								encontrado = false;
								pos = 0;
								ValorAnterior = 0;
								// Acá se aplica el algoritmo de salida, usado hace más tiempo
								for (int ciclo = 0; ciclo < Memoria.Count; ciclo++)
								{
									
									if (ciclo == 0)
									{
										pos = ciclo;
										ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
									}
									else
									{
										// El proceso en esta posición ha sido atendido menos veces que los anteriores,
										// tomamos entonces los valores del nuevo proceso candidato a abandonar la memoria
										if (((ObjetoMemoria)Memoria[ciclo]).Atendido < ValorAnterior)
										{
											pos = ciclo;
											ValorAnterior = ((ObjetoMemoria)Memoria[ciclo]).Atendido;
										}
									}	
								}
								
								Registro += ListarColaProcesos(ColaProcesos);
								// Lo sacamos de la memoria y lo movemos a la cola
								ColaProcesos.Add(Memoria[pos]);
								Registro += "Memoria: " + Environment.NewLine + "Sale <- " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
								tmemuso -= ((ObjetoMemoria)Memoria[pos]).Tamano;
								Memoria.RemoveAt(pos);
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
									TiempoAcumulado -= Convert.ToInt32(((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Substring(1,((ObjetoMemoria)Memoria[pos]).Tiempo.ToString().Length - 1));
									((ObjetoMemoria)Memoria[pos]).Tiempo = 0;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
									Registro += "Tiempo acumulado " + TiempoAcumulado.ToString() + Environment.NewLine;
									Memoria.RemoveAt(pos);
								}
								else
								{
									TiempoAcumulado += quantum;
									((ObjetoMemoria)Memoria[pos]).Tiempo -= quantum;
									Registro += "Memoria: " + Environment.NewLine;
									((ObjetoMemoria)Memoria[pos]).Atendido++;
									Registro += "Atendido -- " + ((ObjetoMemoria)Memoria[pos]).Nombre + " Tiempo restante " + ((ObjetoMemoria)Memoria[pos]).Tiempo + Environment.NewLine;
									if (((ObjetoMemoria)Memoria[pos]).Tiempo == 0)
									{
										Registro += "Termina --  " + ((ObjetoMemoria)Memoria[pos]).Nombre + Environment.NewLine;
										Memoria.RemoveAt(pos);
										incrementar = false;
									}
								}
								
								if (((pos + 1) <= (Memoria.Count - 1)) && (incrementar))
									pos++;
								else
								{
									pos = 0;
									incrementar = true;
								}
							}
						}
					}
					
					Tiempo = 0;
				}
				
				// Le agregamos a la variable registro el total de tiempo utilizado para atender todos los procesos
				Registro += ListarColaProcesos(ColaProcesos);
				Registro += Environment.NewLine + "Tiempo total utilizado: " + TiempoAcumulado.ToString();
			}
			
			return Registro;
		}
	}
}
