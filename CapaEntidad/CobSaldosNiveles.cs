using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CobSaldosNiveles
    {
        private int cdi;

        public int Cdi
        {
            get { return cdi; }
            set { cdi = value; }
        }
        private int id_Cte;

        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }
        private float cartera;

        public float Cartera
        {
            get { return cartera; }
            set { cartera = value; }
        }
        private float pagado;

        public float Pagado
        {
            get { return pagado; }
            set { pagado = value; }
        }
        private float restante;

        public float Restante
        {
            get { return restante; }
            set { restante = value; }
        }

        private string cd_Nombre;

        public string Cd_Nombre
        {
            get { return cd_Nombre; }
            set { cd_Nombre = value; }
        }
    }
}
