/// <summary>
/// Global namespace.
/// </summary>
namespace HW_4_2_08_03_2020
{
    /// <summary>
    /// List with unique elements.
    /// </summary>
    public class UniqueList : List
    {
        /// <summary>
        /// Adds element to position in list.
        /// </summary>
        /// <param name="value">Element to add.</param>
        /// <param name="position">Position of new element in list.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Throws at the wrong position.</exception>
        /// <exception cref="ElementAlreadyExistExceprtion">Throws when element already exist.</exception>
        public override void AddElement(int value, int position)
        {
            if (this.IsItemExists(value))
            {
                throw new ElementAlreadyExistExceprtion($"Element {value} already exist.");
            }
            else
            {
                base.AddElement(value, position);
            }
        }
    }
}
