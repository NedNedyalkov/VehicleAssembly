using TireFittingShop.Domain;

namespace TireFittingShop.Abstractions
{
    /// <summary>
    /// Factory responsible for creating new <see cref="Customer"/> instances.
    /// </summary>
    public interface ICustomerFactory
    {
        /// <summary>
        /// Creates a new customer instance.
        /// </summary>
        /// <returns>A fresh <see cref="Customer"/>.</returns>
        Customer Create();
    }
}
