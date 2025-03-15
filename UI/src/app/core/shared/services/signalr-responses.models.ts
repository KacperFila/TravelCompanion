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
  from: string; // Date in string format, you can change this to Date type if you want to convert it later
  to: string; // Same for this
  additionalCosts: any[]; // Adjust type as needed
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
  events: any[]; // Adjust as needed
}
