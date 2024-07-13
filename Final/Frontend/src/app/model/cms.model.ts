export class CMS{
  id:number=0;
  title:string='';
  description:string='';
  slug:string='';
  status:string='';
}


export class Mission{
  id:number;
  missionTitle:string='';
  missionDescription:string='';
  missionIntroduction:string;
  missionChallenge:string;
  totalSheets:number;
  seatsLeft:number;
  startDate:any;
  endDate:any;
  registrationDeadLine:any;
  missionThemeId:number;
  missionOrganisationId:number;
  cityId:number=0;
  countryId:number=0;
  missionImages:any;
  missionType:any;
  missionObject:string;
  missionAchived:string=''; 
}


export class Country {
  id:number=0;
  countryName:string='';
}

export class City {
  id:number=0;
  countryId:number=0;
  cityName:string='';
}
