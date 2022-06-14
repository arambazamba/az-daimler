import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Skill } from '../skill.model';

@Component({
  selector: 'app-skills-list',
  templateUrl: './skills-list.component.html',
  styleUrls: ['./skills-list.component.scss'],
})
export class SkillsListComponent implements OnInit {
  @Input() skills: Skill[];
  @Output() onSkillSelected: EventEmitter<Skill> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  selectSkill(item: Skill) {
    this.onSkillSelected.emit(item);
  }
}
