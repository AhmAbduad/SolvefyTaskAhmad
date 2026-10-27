
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

interface EquipmentStat {
  title: string;
  value: string;
  detail: string;
  icon: string;
  color: string;
}

interface EquipmentStatus {
  label: string;
  value: number;
  color: string;
}

interface Activity {
  title: string;
  time: string;
  icon: string;
  type: 'success' | 'danger' | 'default';
}

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.css'
})
export class DashboardHome {

  equipmentStats: EquipmentStat[] = [
    {
      title: 'Total Equipment',
      value: '25',
      detail: 'All registered equipment',
      icon: '▣',
      color: 'blue'
    },
    {
      title: 'Available',
      value: '15',
      detail: 'Ready for checkout',
      icon: '✓',
      color: 'green'
    },
    {
      title: 'Checked Out',
      value: '7',
      detail: 'Currently in use',
      icon: '↗',
      color: 'orange'
    },
    {
      title: 'Under Repair',
      value: '3',
      detail: 'Requires maintenance',
      icon: '⚙',
      color: 'red'
    }
  ];

  equipmentStatus: EquipmentStatus[] = [
    { label: 'Available', value: 15, color: '#22c55e' },
    { label: 'Checked Out', value: 7, color: '#f59e0b' },
    { label: 'Under Repair', value: 3, color: '#ef4444' }
  ];

  showAllActivities = false;

  activities: Activity[] = [
    {
      title: 'GPS Rover checked out by Ahmad',
      time: 'Today',
      icon: '↗',
      type: 'success'
    },
    {
      title: 'Total Station returned',
      time: 'Today',
      icon: '✓',
      type: 'success'
    },
    {
      title: 'Drone requires approval',
      time: 'Yesterday',
      icon: '!',
      type: 'danger'
    },
    {
      title: 'Field Tablet added',
      time: 'Yesterday',
      icon: '+',
      type: 'default'
    }
  ];

  get visibleActivities(): Activity[] {
    return this.showAllActivities
      ? this.activities
      : this.activities.slice(0, 3);
  }

  toggleActivities(): void {
    this.showAllActivities = !this.showAllActivities;
  }
}
