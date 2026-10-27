import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Dashboard } from './components/dashboard/dashboard';
import { Equipment } from './components/equipment/equipment';
import { DashboardHome } from './components/dashboard-home/dashboard-home';
import { Checkout } from './components/checkout/checkout';
import { Checkin } from './components/checkin/checkin';
import { Approvals } from './components/approvals/approvals';
import { Myequipment } from './components/myequipment/myequipment';
import { Damagereports } from './components/damagereports/damagereports';
import { Reports } from './components/reports/reports';
import { TeamMembers } from './components/teamsmembers/teamsmembers';


export const routes: Routes = [
   { path: 'login', component: Login },
  {
    path: '',
    component: Dashboard,
    children: [
      { path: 'dashboard', component: DashboardHome },
      { path: 'equipment', component: Equipment },
      { path: 'check-out', component: Checkout },
      { path: 'check-in', component: Checkin },
      { path: 'approvals', component: Approvals },
      { path: 'approvals/:id',component: Approvals},
      { path: 'my-equipment', component: Myequipment },
      { path: 'damage-reports', component: Damagereports },
      { path: 'reports', component: Reports },
       {path: 'team-members',component: TeamMembers},
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'login' }
];
