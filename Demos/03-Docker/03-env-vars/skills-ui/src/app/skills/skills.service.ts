import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Skill } from './skill.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SkillsService {
  constructor(public client: HttpClient) {}

  getSkills() {
    return this.client.get<Skill[]>(environment.apiUrl);
  }
}
