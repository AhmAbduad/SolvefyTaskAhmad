import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
   imports: [RouterOutlet],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
private router = inject(Router);

  equipmentStats = [
    {
      title: 'Total Equipment',
      value: 25,
      icon: '▣',
      color: 'blue',
      detail: 'Registered assets'
    },
    {
      title: 'Checked Out',
      value: 12,
      icon: '▤',
      color: 'red',
      detail: 'Currently in use'
    },
    {
      title: 'Available',
      value: 10,
      icon: '▦',
      color: 'green',
      detail: 'Ready to use'
    },
    {
      title: 'Pending Approval',
      value: 2,
      icon: '♧',
      color: 'yellow',
      detail: 'Awaiting review'
    }
  ];

  equipmentStatus = [
    { label: 'Available', value: 10, color: '#119c78' },
    { label: 'Checked Out', value: 12, color: '#b98d61' },
    { label: 'Pending Approval', value: 2, color: '#f4bd45' },
    { label: 'Under Repair', value: 1, color: '#f28b47' }
  ];

  activities = [
    {
      type: 'success',
      title: 'Ali Raza took GPS Rover-01',
      time: '2 hours ago',
      icon: '↗'
    },
    {
      type: 'success',
      title: 'Usman returned Total Station-02',
      time: '5 hours ago',
      icon: '↙'
    },
    {
      type: 'danger',
      title: 'Damage reported for Field Tablet',
      time: '1 day ago',
      icon: '!'
    },
    {
      type: 'danger',
      title: 'Drone request pending approval',
      time: '1 day ago',
      icon: '!'
    }
  ];

  menuItems = [
    { label: 'Dashboard', icon: '⌂', active: true },
    { label: 'Equipment', icon: '▦', active: false },
    { label: 'Check Out', icon: '↗', active: false },
    { label: 'Check In', icon: '↙', active: false },
    { label: 'Approvals', icon: '☑', active: false },
    { label: 'My Equipment', icon: '♙', active: false },
    { label: 'Damage Reports', icon: '⚒', active: false },
    { label: 'Reports', icon: '▤', active: false },
    { label: 'Team Members', icon: '♧', active: false }
  ];

  selectedMenu = 'Dashboard';
  showAllActivities = false;
  showProfileMenu = false;

  get visibleActivities() {
    return this.showAllActivities
      ? this.activities
      : this.activities.slice(0, 4);
  }

 selectMenu(label: string): void {
    const routes: Record<string, string> = {
      Dashboard: '/dashboard',
      Equipment: '/equipment',
      'Check Out': '/check-out',
      'Check In': '/check-in',
      Approvals: '/approvals',
      'My Equipment': '/my-equipment',
      'Damage Reports': '/damage-reports',
      Reports: '/reports',
      'Team Members': '/team-members'
    };

    const path = routes[label];

    if (path) {
      this.router.navigate([path]);
    }

    if (label === 'Check Out') {
      this.router.navigate(['/check-out']);
    }
  }

  toggleActivities(): void {
    this.showAllActivities = !this.showAllActivities;
  }

  toggleProfile(): void {
    this.showProfileMenu = !this.showProfileMenu;
  }
}
