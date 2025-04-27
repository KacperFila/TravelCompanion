export interface CreateTravelPlanRequest {
  title: string;
  description: string | null;
  from: string | null;
  to: string | null;
}

export interface TravelPlan {
  id: string;
  ownerId: string;
  participants: string[];
  title: string;
  description: string | null;
  from: string;
  to: string;
  additionalCostsValue: number;
  totalCostValue: number;
  planStatus: string;
  travelPlanPoints: TravelPoint[];
}

export interface TravelPoint {
  id: string;
  placeName: string;
  totalCost: number;
  travelPlanOrderNumber: number;
}

export interface TravelPointUpdateRequest {
  requestId: string;
  travelPlanPointId: string;
  suggestedById: string;
  placeName: string;
  createdOnUtc: string;
  modifiedOnUtc: string;
}

export interface UpdateRequestUpdateResponse {
  updateRequests: TravelPointUpdateRequest[];
  pointId: string;
}

export interface PlanParticipant {
  id: string;
  email: string;
}

