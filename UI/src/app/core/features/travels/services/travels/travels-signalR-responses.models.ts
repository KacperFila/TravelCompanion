export interface UpdatedPlan {
  ownerId: {
    value: string;
  };
  participants: Array<{
    value: string;
  }>;
  participantPaidIds: string[];
  title: string;
  description: string;
  from: string;
  to: string;
  additionalCosts: any[];
  additionalCostsValue: {
    amount: number;
    currency: string;
  };
  totalCostValue: {
    amount: number;
    currency: string;
  };
  travelPlanPoints: Array<{
    id: {
      value: string;
    };
    totalCost: {
      amount: number;
      currency: string;
    };
    planId: {
      value: string;
    };
    placeName: string;
    travelPlanOrderNumber: number;
  }>;
  doesAllParticipantsPaid: boolean;
  doesAllParticipantsAccepted: boolean;
  planStatus: string;
  createdOnUtc: string;
  modifiedOnUtc: string;
  id: {
    value: string;
  };
  version: number;
  events: any[];
}

export interface PlanInvitationResponse
{
  invitationId: string,
  planId: string,
  inviterName: string,
  planTitle: string,
  invitationDate: string
}

export interface PlanInvitationRemovedResponse
{
  invitationId: string
}
