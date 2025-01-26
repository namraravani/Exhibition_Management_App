import 'package:flutter/material.dart';

class FilterBtn {
  static void showFilterDialog(BuildContext context) {
    final TextEditingController startDateController = TextEditingController();
    final TextEditingController endDateController = TextEditingController();

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text("Filter by Date"),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextField(
                controller: startDateController,
                decoration: const InputDecoration(
                    labelText: "Start Date",
                    hintText: "Enter start date",
                    fillColor: Colors.grey),
              ),
              const SizedBox(height: 16),
              TextField(
                controller: endDateController,
                decoration: const InputDecoration(
                    labelText: "End Date",
                    hintText: "Enter end date",
                    fillColor: Colors.grey),
              ),
            ],
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context), // Close the dialog
              child: const Text("Cancel"),
            ),
            ElevatedButton(
              onPressed: () {
                // Handle OK button action
                String startDate = startDateController.text;
                String endDate = endDateController.text;

                print("Start Date: $startDate");
                print("End Date: $endDate");

                Navigator.pop(context); // Close the dialog
              },
              child: const Text("OK"),
            ),
          ],
        );
      },
    );
  }
}
