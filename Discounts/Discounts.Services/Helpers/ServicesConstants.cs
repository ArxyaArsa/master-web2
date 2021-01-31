using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Helpers
{
    public static class ServicesConstants
    {
        public const string DeleteNotAllowedReasonMessage_UserHasUsedActions = "Invalid operation. This user has Used Actions and cannot be deleted as such. Either simply disable it or first delete all UsedAction records tied to the user.";
    }
}
