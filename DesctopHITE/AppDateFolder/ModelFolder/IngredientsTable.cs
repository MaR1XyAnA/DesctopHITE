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
    
    public partial class IngredientsTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IngredientsTable()
        {
            this.MenuTable = new HashSet<MenuTable>();
        }
    
        public int PersonalNumber_Ingredients { get; set; }
        public string Name_Ingredients { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuTable> MenuTable { get; set; }
    }
}
