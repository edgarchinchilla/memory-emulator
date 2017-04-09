/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 03:05 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace MemoryEmulator
{
	/// <summary>
	/// Description of Conexión.
	/// </summary>
	public class Conexión
	{
		string servidorBD = String.Empty;
		string baseDatos = String.Empty;
		string usuario = String.Empty;
		string contrasena =  String.Empty;
		string SqlCon;
		
		public Conexión()
		{
			// Algo jejeje
		}
		public string Obtener()
		{
			// Leemos los valores de conexión del archivo de configuración
			AppSettingsReader Configuración = new AppSettingsReader();
			servidorBD = (string)Configuración.GetValue("servidor",typeof(string));
			baseDatos = (string)Configuración.GetValue("bd",typeof(string));
			usuario = (string)Configuración.GetValue("usuario",typeof(string));
			contrasena = (string)Configuración.GetValue("contrasena",typeof(string));
			
			// Creamos el string de conexión
			SqlCon = String.Format("user id={0}; password={1}; server={2}; Trusted_Connection=yes; database={3}; connection timeout=30;",usuario,contrasena,servidorBD,baseDatos);
			
			return SqlCon;
		}
	}
}
