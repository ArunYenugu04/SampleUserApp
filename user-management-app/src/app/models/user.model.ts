export interface Address {
    streetAddress: string;
    city: string;
    state: string;
    postalCode: string;
  }
  
  export interface Telephone {
    phoneNumber: string;
    type: string;
  }
  
  export interface Institution {
    name: string;
    city: string;
    state: string;
    domain: string;
  }
  
  export interface UserInstitution {
    institutionId: number;
    institutions: Institution;
  }
  
  export interface User {
    id: number;
    firstname: string;
    lastname: string;
    email: string;
    birthdate: string;
    address: Address;
    telephones: Telephone;
    userInstitutions: UserInstitution[];
  }