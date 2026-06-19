using Kernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Domain.Members.Entities
{
    // Member Id guarda un numero entero dentro de propiedad value
    public record MemberId(int Value) : IValue<int>
    {
        // static: pertenece a la clase/tipo, no a un objeto concreto.
        // implicit: permite convertir automáticamente sin escribir .Value.
        // operator:  define una conversión personalizada entre tipos.
        // Significa que MemberId ya es un int
        public static implicit operator int(MemberId self) => self.Value;
    }
}
