using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Helpers
{
    public static class ServicesConstants
    {
        public const string DeleteUser_NotAllowedReasonMessage_UserHasUsedActions = "Invalid operation. This user has Used Actions and as such cannot be deleted. Either simply disable it or first delete all UsedAction records tied to the user.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasUsers = "Invalid operation. This partner has Uses tied to it and as such cannot be deleted. First delete all User references to it.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasUsedActions = "Invalid operation. This partner has Used Actions and as such cannot be deleted. First delete all UsedAction records tied to the user.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasActions = "Invalid operation. This partner has Actions and as such cannot be deleted. First delete all Action records tied to the user.";
    }
}
