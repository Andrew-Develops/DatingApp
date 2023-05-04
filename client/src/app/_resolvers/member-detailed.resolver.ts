import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { MembersService } from '../_services/members.service';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MemberDetailedResolver implements Resolve<Member> {
  constructor(private memeberService: MembersService) {
    
  }
  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.memeberService.getMember(route.paramMap.get('username')!)
  }
}
