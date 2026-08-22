export interface Employee{
  id : number;
  firstName: string;
  lastName: string;
  positionId: number;
  positionName: string;
  birthDate: Date;
  startDate: Date;
  endDate ?: Date;
  phoneNumber: string;

  managerId?: number | null; // number, undefined, null
  managerName?: string | null;

  departmentId?: number;
  departmentName?: string;
  salary?: number;
  email?: string;
  userId?: number;
  isActive: boolean;
}