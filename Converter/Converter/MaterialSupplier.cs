//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Converter
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialSupplier
    {
        public int ID { get; set; }
        public int Material { get; set; }
        public int Supplier { get; set; }
    
        public virtual Material Material1 { get; set; }
        public virtual Supplier Supplier1 { get; set; }
    }
}
