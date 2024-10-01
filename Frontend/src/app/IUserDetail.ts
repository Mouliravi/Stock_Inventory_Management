export interface IUserDetail {
  UserId?: number;
  UserName: string;
  DateOfBirth: string; // Assuming it's a string representation of a date
  AddressLine1: string;
  MobileNumber: string;
  Occupation: string;
  Email: string;
  AnnualIncome: number;
  BalanceAmount: number;
  BankDetailsId?: number;
  City: {
    CityId : number;
    City: string;
  };
  Country: {
    CountryId : number;
    Country: string;
  };
  Gender: {
    GenderId : number;
    Gender: string;
  };
  MaritalStatus: {
    MaritalStatusId : number;
    MaritalStatus: string;
  };
  Nationality: {
    NationalityId : number;
    Nationality: string;
  };
  Role: {
    RoleId: number;
    Role: string;
  };
  State: {
    StateId : number;
    State: string;
  };
}
