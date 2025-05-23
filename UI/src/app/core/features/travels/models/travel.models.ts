export interface TravelDetailsDto {
  id: string;
  title: string;
  description: string | null;
  from: string | null;
  to: string | null;
  isFinished: boolean;
  rating: number | null;
  additionalCostsValue: number | null;
  totalCostValue: number | null;
  travelPlanPoints: TravelPointDto[];
}

export interface TravelPointDto {
  id: string;
  placeName: string;
  totalCost: number;
  travelPlanOrderNumber: number;
}
