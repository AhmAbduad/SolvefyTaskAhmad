
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {
  CreateEquipmentRequest,
  Equipment as EquipmentModel
} from '../../models/equipment';
import { EquipmentService } from '../../services/equipment';

interface Equipments {
  id: number;
  name: string;
  category: string;
  serialNo: string;
  type: string;
  status: string;
  assignedTo: string;
}

@Component({
  selector: 'app-equipment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './equipment.html',
  styleUrl: './equipment.css',
})
export class Equipment implements OnInit {
  private equipmentService = inject(EquipmentService);

  ngOnInit(): void {
    this.loadEquipment();
  }

  searchText = '';
  selectedCategory = 'All Categories';
  selectedStatus = 'All Status';
  showFilters = false;
  showAddForm = false;
  selectedEquipment: number | null = null;

  loading = false;
  saving = false;
  errorMessage = '';
  successMessage = '';

  categories = [
    'All Categories',
    'Total Station',
    'GPS',
    'Drone',
    'Tripod',
    'Tablet'
  ];

  statuses = [
    'All Status',
    'Available',
    'Checked Out',
    'Approval Required',
    'Under Repair'
  ];

  // Bound to your Add Equipment form
  newEquipment = {
    name: '',
    category: '',
    serialNo: '',
    type: 'Individual'
  };

  equipment: Equipments[] = [];

  // GET: Fetch equipment from the ASP.NET Core API
  loadEquipment(): void {
    this.loading = true;
    this.errorMessage = '';

    this.equipmentService.getAllEquipment().subscribe({
      next: (data: EquipmentModel[]) => {
        this.equipment = data.map(item => ({
          id: item.id,
          name: item.name,
          category: item.category,
          serialNo: item.serialNumber ?? '-',
          type: item.type,
          status: item.status,
          assignedTo: item.assignedTo ?? '-'
        }));

        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading equipment:', error);
        this.errorMessage = 'Unable to load equipment. Please try again.';
        this.loading = false;
      }
    });
  }

  // Search and filter equipment
  get filteredEquipment(): Equipments[] {
    const search = this.searchText.trim().toLowerCase();

    return this.equipment.filter(item => {
      const matchesSearch =
        !search ||
        item.name.toLowerCase().includes(search) ||
        item.category.toLowerCase().includes(search) ||
        item.serialNo.toLowerCase().includes(search) ||
        item.assignedTo.toLowerCase().includes(search);

      const matchesCategory =
        this.selectedCategory === 'All Categories' ||
        item.category === this.selectedCategory;

      const matchesStatus =
        this.selectedStatus === 'All Status' ||
        (this.selectedStatus === 'Available'
          ? item.status.startsWith('Available')
          : item.status === this.selectedStatus);

      return matchesSearch && matchesCategory && matchesStatus;
    });
  }

  // POST: Insert equipment using the values entered in the form
 addEquipment(
  name: string,
  category: string,
  serialNo: string,
  type: string
): void {
  if (!name.trim() || !category || !type) {
    this.errorMessage = 'Please enter the name, category, and type.';
    return;
  }

  const request: CreateEquipmentRequest = {
    name: name.trim(),
    category,
    serialNumber: serialNo.trim() || null,
    type
  };

  this.saving = true;
  this.errorMessage = '';
  this.successMessage = '';
  this.showAddForm = false;
  this.equipmentService.createEquipment(request).subscribe({
    next: () => {
      
      this.saving = false;
      this.showAddForm = false;
      this.successMessage = 'Equipment added successfully.';
      this.loadEquipment();
    },
    error: (error) => {
      console.error('Error creating equipment:', error);
      this.saving = false;
      this.errorMessage =
        typeof error?.error === 'string'
          ? error.error
          : error?.error?.message || 'Unable to add equipment.';
    }
  });
}

  toggleFilters(): void {
    this.showFilters = !this.showFilters;
  }

  toggleActions(id: number): void {
    this.selectedEquipment =
      this.selectedEquipment === id ? null : id;
  }

  clearFilters(): void {
    this.searchText = '';
    this.selectedCategory = 'All Categories';
    this.selectedStatus = 'All Status';
  }

  trackById(_index: number, item: Equipments): number {
    return item.id;
  }
}
