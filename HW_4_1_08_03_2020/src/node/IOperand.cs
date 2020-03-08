/// <summary>
/// Global namespace.
/// </summary>
namespace HW_4_1_08_03_2020
{
    /// <summary>
    /// Operand interface.
    /// </summary>
    public interface IOperand
    {
        /// <summary>
        /// Get operand value.
        /// </summary>
        /// <returns>Operand value.</returns>
        public Value GetValue();

        /// <summary>
        /// Print operand function.
        /// </summary>
        public void Print();
    }
}
