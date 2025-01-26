class ExhibitionCardDTO {
  final String imageUrl;
  final String venue;
  final String startDate;
  final String endDate;
  final String eventStatus;

  ExhibitionCardDTO({
    required this.imageUrl,
    required this.venue,
    required this.startDate,
    required this.endDate,
    required this.eventStatus,
  });
}
