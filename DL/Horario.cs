﻿using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Horario
    {
        public int IdHorario { get; set; }
        public string? Nombre { get; set; }
        public int? IdGrupo { get; set; }

        public virtual Grupo? IdGrupoNavigation { get; set; }
    }
}
