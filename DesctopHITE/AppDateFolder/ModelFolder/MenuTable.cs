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
    
    public partial class MenuTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuTable()
        {
            this.ChequeTable = new HashSet<ChequeTable>();
            this.IngredientsTable = new HashSet<IngredientsTable>();
        }
    
        public int PersonalNumber_Menu { get; set; }
        public string Name_Menu { get; set; }
        public string Description_Menu { get; set; }
        public int pnImageMunu_Menu { get; set; }
        public int pnMenuCategory_Menu { get; set; }
        public decimal Prise_Menu { get; set; }
    
        public virtual ImageMenuTable ImageMenuTable { get; set; }
        public virtual MenuCategoryTable MenuCategoryTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChequeTable> ChequeTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientsTable> IngredientsTable { get; set; }
    }
}
