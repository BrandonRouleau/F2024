using System.ComponentModel.DataAnnotations;

namespace brH60Store.Validation {
    public class ModelValidator {
        public bool ValidateStock(int stock, int update) {
            if (update < 0) {
                if(stock + update < 0) return false;

                return true;
            }
            return true;
        }

        public bool ValidateNewPrice(decimal price) {
            if (price < 0) return false;
            return true;
        }

        public bool ValidateBuySellPrice(decimal buyPrice, decimal sellPrice) {
            if (sellPrice < buyPrice) return false;
            return true;
        }
    }
}
