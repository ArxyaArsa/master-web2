﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Helpers
{
    public static class ServicesConstants
    {
        public const string DeleteUser_NotAllowedReasonMessage_UserHasUsedActions = "Invalid operation. This User has Used Actions and as such cannot be deleted. Either simply disable it or first delete all UsedAction records tied to the user.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasUsers = "Invalid operation. This Partner has Uses tied to it and as such cannot be deleted. First delete all User references to it.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasUsedActions = "Invalid operation. This Partner has Used Actions and as such cannot be deleted. First delete all UsedAction records tied to it.";
        public const string DeletePartner_NotAllowedReasonMessage_PartnerHasActions = "Invalid operation. This Partner has Actions and as such cannot be deleted. First delete all Action records tied to it.";
        public const string DeletePartnerType_NotAllowedReasonMessage_PartnerTypeHasPartners = "Invalid operation. This Partner Pype has Partners and as such cannot be deleted. First delete all references tied to it.";
        public const string CreatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists = "Invalid operation. A mapping with this Partner and Action already exists.";
        public const string CreatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist = "Invalid operation. No Partner found with supplied Id.";
        public const string CreatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist = "Invalid operation. No Action found with supplied Id.";
        public const string UpdatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists = "Invalid operation. A mapping with this Partner and Action already exists.";
        public const string UpdatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist = "Invalid operation. No Partner found with supplied Id.";
        public const string UpdatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist = "Invalid operation. No Action found with supplied Id.";
        public const string CreateUsedAction_NotAllowedReasonMessage_MapAlreadyExists = "Invalid operation. A mapping with this User, Partner and Action already exists.";
        public const string CreateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist = "Invalid operation. No Partner found with supplied Id.";
        public const string CreateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist = "Invalid operation. No Action found with supplied Id.";
        public const string CreateUsedAction_NotAllowedReasonMessage_UserDoesNotExist = "Invalid operation. No User found with supplied Id.";
        public const string UpdateUsedAction_NotAllowedReasonMessage_MapAlreadyExists = "Invalid operation. A mapping with this User, Partner and Action already exists.";
        public const string UpdateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist = "Invalid operation. No Partner found with supplied Id.";
        public const string UpdateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist = "Invalid operation. No Action found with supplied Id.";
        public const string UpdateUsedAction_NotAllowedReasonMessage_UserDoesNotExist = "Invalid operation. No User found with supplied Id.";
    }
}
