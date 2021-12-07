using System;

namespace DeepSpace
{

	class Movimiento
	{

		public Movimiento(Planeta o, Planeta d)
		{
			
			this.origen=o;
			this.destino=d;

		}
		public Planeta origen { //get y set del planeta origen del movimiento.
			get;
			set;
		}
		public Planeta destino { //get y set del planeta destino del movimiento.
			get;
			set;
			
		}
		

	}
	
	
}

