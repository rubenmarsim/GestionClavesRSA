﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GestionRSA
{
    public class GestionRSA
    {
        #region Variables Globales
        /// <summary>
        /// Clase principal que nos permite usar el RSA
        /// </summary>
        RSACryptoServiceProvider _RSA;
        /// <summary>
        /// Clase principal que nos permite usar el RSA, en este caso lo vamos a usar 
        /// para guardar la clave que leamos del XML
        /// </summary>
        RSACryptoServiceProvider _RSAEnc;
        /// <summary>
        /// Representa los parámetros estándar para el algoritmo System.Security.Cryptography.RSA.
        /// </summary>
        RSAParameters _RSAParams;
        /// <summary>
        /// Variable donde vamos a guardar la clave publica
        /// </summary>
        string _publicKey = string.Empty;
        /// <summary>
        /// Variable donde vamos a guardar la clave privada
        /// </summary>
        string _privateKey = string.Empty;
        /// <summary>
        /// Constante con la ruta donde vamos a tener nuestros archivos encriptados
        /// </summary>
        string _PathArchivos = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Archivos/";
        /// <summary>
        /// Path donde se guarda la dll del programa
        /// </summary>
        string _PathAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        /// <summary>
        /// Constante para el uso de la extension de XML
        /// </summary>
        const string _XMLExtension = ".xml";
        /// <summary>
        /// Objeto que contiene el KeyContainer que nos va a servir para 
        /// crear claves persistentes
        /// </summary>
        CspParameters _CSPP;
        /// <summary>
        /// Nombr que le vamos a dar al KeyContainerName
        /// </summary>
        const string _KeyName = "Key01";
        /// <summary>
        /// Variable donde vamos a guardar la clave RSA publica que vamos a leer del XML
        /// </summary>
        string _XMLPublicKey = string.Empty;
        /// <summary>
        /// Variable donde guardamos la clave RSA publica que cogemos de la DB
        /// </summary>
        string _PublicKeyDB = string.Empty;

        ConnectionClass.ConnectionClass CClassDB;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GestionRSA()
        {
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Metodo para crear y guardar las claves RSA
        /// </summary>
        /// <param name="IsPersistent">bool para decidir si queremos que las claves sean persistentes o no</param>
        /// <param name="bWriteToXML">bool para decidir si queremos printar las claves en archivos XML</param>
        public void CreateAndWriteRSAKeys(bool IsPersistent = true, bool bWriteToXML = true)
        {
            if (IsPersistent)
            {
                ///Instanciamos el keycontainer
                _CSPP = new CspParameters();
                ///le damos un nombre al keycontainer
                _CSPP.KeyContainerName = _KeyName;
                ///Instanciamos otra vez el RSA pero esta vez le pasamos el parametro CSP
                ///para poder hacer la clave persistente
                _RSA = new RSACryptoServiceProvider(_CSPP);
                ///Le decimos que la clave sea persistente
                _RSA.PersistKeyInCsp = true;
            }
            else
            {
                ///Esto genera un par de claves publica/privada
                _RSA = new RSACryptoServiceProvider();
            }
            ///Guardamos la info de las claves a una estructura RSAParameters
            ///Al poner false excluimos los parametros privados
            _RSAParams = _RSA.ExportParameters(false);
            ///Crea y devuelve una cadena XML que contiene la clave del objeto 
            ///System.Security.Cryptography.RSA actual.
            ///Al poner false en el parametro hacemos que solo devuelva la clave publica
            _publicKey = _RSA.ToXmlString(false);
            ///Crea y devuelve una cadena XML que contiene la clave del objeto 
            ///System.Security.Cryptography.RSA actual.
            ///Al poner true en el parametro hacemos que devuelva la clave publica y privada
            _privateKey = _RSA.ToXmlString(true);

            if (bWriteToXML)
            {
                ///Escribimos la clave publica en su respectivo archivo XML
                File.WriteAllText(_PathArchivos + "PublicKey" + _XMLExtension, _publicKey);
                ///Escribimos la clave privada en su respectivo archivo XML
                File.WriteAllText(_PathArchivos + "PrivateKey" + _XMLExtension, _privateKey);
            }
        }
        /// <summary>
        /// Leemos la clave publica desde un fichero XML
        /// </summary>
        private void ReadRSAKeys()
        {
            try
            {
                _RSAEnc = new RSACryptoServiceProvider();
                _XMLPublicKey = File.ReadAllText(_PathArchivos + "PublicKey" + _XMLExtension);
                _RSAEnc.FromXmlString(_XMLPublicKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Leemos el string que contien la clave publica que hemos guardado en la Base de datos
        /// </summary>
        public void ReadRSAKeysFromDB()
        {
            CClassDB = new ConnectionClass.ConnectionClass();

            var dts = CClassDB.PortaPerConsulta("SELECT XMLKey FROM PlanetKeys");
            var UltimaFila = dts.Tables[0].Rows.Count - 1;
            _PublicKeyDB = dts.Tables[0].Rows[UltimaFila].ItemArray[0].ToString();
        }
        /// <summary>
        /// Guardamos el string de la clave publica en la base de datos
        /// </summary>
        public void GuardarPublicKeyEnDB()
        {
            ReadRSAKeys();
            CClassDB = new ConnectionClass.ConnectionClass();

            CClassDB.Executa("insert into PlanetKeys (Planet, XMLKey) values ('PbcK', '" + _XMLPublicKey + "');");
        }
        public byte[] EncriptarRSA(string txt, string xmlPublic)
        {
            _RSA.FromXmlString(xmlPublic);
            byte[] encrypt = _RSA.Encrypt(Encoding.ASCII.GetBytes(txt), false);
            return encrypt;
        }
        public byte[] DesencriptarRSA(string txt, string xmlPrivada)
        {
            _RSA.FromXmlString(xmlPrivada);
            byte[] dencrypt = _RSA.Decrypt(Convert.FromBase64String(txt), false);
            return dencrypt;
        }
        #endregion
    }
}
