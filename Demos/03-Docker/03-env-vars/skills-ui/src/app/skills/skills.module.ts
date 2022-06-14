import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { SkillsRoutingModule } from './skills-routing.module';
import { SkillsComponent } from './skills.component';
import { SkillsListComponent } from './skills-list/skills-list.component';

@NgModule({
  declarations: [SkillsComponent, SkillsListComponent],
  imports: [CommonModule, SkillsRoutingModule, HttpClientModule],
})
export class SkillsModule {}
