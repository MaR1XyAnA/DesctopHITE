//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesctopHITE.AppDateFolder.ModelFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableEventsInConstructors")]
        public StatusTable()
        {
            this.WorkerTable = new HashSet<WorkerTable>();
        }
    
        public int PersonalNumber_Status { get; set; }
        public string Title_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkerTable> WorkerTable { get; set; }
    }
}
